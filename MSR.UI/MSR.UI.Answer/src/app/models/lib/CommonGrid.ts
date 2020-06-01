import { Injectable } from '@angular/core';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';
import { ColumnsSaved } from './ColumnsSaved';

@Injectable()
export class CommonGrid {
    views: Array<ViewSaved>;

    constructor(private toastr: ToastrService) {
        this.loadViewsFromLocalStorage();
    }

    loadViewsFromLocalStorage() {
        const savedViews = localStorage.getItem('viewsSaved');
        if (savedViews !== undefined && savedViews !== null) {
            this.views = JSON.parse(savedViews).map(x => {
                if (Array.isArray(x.columns)) {
                    x.columns = x.columns.map(y => new ColumnsSaved(y))
                }
                return new ViewSaved(x);
            });
            this.setDefaultViews();
        } else {
            this.views = new Array<ViewSaved>();
        }
    }

    isVisibleCol(id: string, gridSettings: ColumnsSaved[]) {
        return gridSettings.filter(x => x.id === id)[0].visible;
    }

    filter(table, field: string) {
        if (table.filterTimeout) {
            clearTimeout(table.filterTimeout);
        }

        if (table.filters[field]) {
            delete table.filters[field];
        }

        table.filterTimeout = setTimeout(() => {
            table._filter();
            table.filterTimeout = null;
        }, table.filterDelay);

        table.anchorRowIndex = null;
    }

    defPlaceholder(grid, id: string, arr, defaultLabel: string) {
        const ret = grid?.filters[id]?.value;
        if (ret === undefined) {
            return defaultLabel;
        }

        return arr.find(x => x.value === ret).label;
    }

    addView(view: ViewSaved) {
        if (this.views.findIndex(x => x.viewName === view.viewName && x.gridId === view.gridId) !== -1) {
            this.toastr.error(`Sorry a view with the name ${view.viewName} alread exists.`);
            return;
        }
        view.gridPagingData = localStorage.getItem(view.gridId);
        if (view.gridPagingData === null) {
            this.toastr.error(`Sorry there are no filters applied to the grid to save this as a template.`);
            return;
        }
        this.views.push(view);
        if (view.isDefault) {
            this.setAsDefault(view, false);
        }
        else {
            localStorage.setItem('viewsSaved', JSON.stringify(this.views));
        }
    }

    deleteView(view: ViewSaved) {
        const index = this.views.findIndex(x => x.viewName === view.viewName && x.gridId === view.gridId);
        if (index !== -1) {
            this.views.splice(index, 1);
            localStorage.setItem('viewsSaved', JSON.stringify(this.views));
            this.toastr.success(`View ${view.viewName} was successfully removed.`)
        }
        else {
            this.toastr.error(`View ${view.viewName} not found.`)
        }
    }

    setAsDefault(view: ViewSaved, showSuccess: boolean = true) {
        const views = this.views.filter(x => x.gridId === view.gridId);
        if (views.length === 0) {
            this.toastr.error(`View ${view.viewName} not found.`);
            return
        }
        views.forEach(x => {
            x.isDefault = view.viewName === x.viewName && view.isDefault;
        });
        localStorage.setItem('viewsSaved', JSON.stringify(this.views));
        if (showSuccess) {
            this.toastr.success(`View ${view.viewName} status changed to ${view.isDefault ? 'Default' : 'Not Default'}.`);
        }
    }

    updateView(templateView: ViewSaved, currentView: ViewSaved) {
        let view = this.views.find(x => x.gridId === templateView.gridId && x.viewName === templateView.viewName);
        view.columns = currentView.columns;
        view.gridPagingData = localStorage.getItem(view.gridId);
        view.pagingTotal = currentView.pagingTotal;
        view.version = currentView.version;
        localStorage.setItem('viewsSaved', JSON.stringify(this.views));
        this.toastr.success(`View ${view.viewName} has been updated to current view.`);
    }

    getViews(gridId: string): Array<ViewSaved> {
        const views = this.views.filter(x => x.gridId === gridId);
        return views;
    }

    setDefaultViews() {
        this.views.forEach((view) => {
            if (view.isDefault) {
                localStorage.setItem(view.gridId, view.gridPagingData);
            }
        });
    }

    getDefaultView(gridId: string): ViewSaved {
        const viewIndex = this.views.findIndex(x => x.gridId === gridId && x.isDefault);
        if (viewIndex === -1) {
            localStorage.removeItem(gridId);
            return null;
        }
        else {
            const view = this.views[viewIndex];
            localStorage.setItem(gridId, view.gridPagingData);
            return view;
        }
    }

}