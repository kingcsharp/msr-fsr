describe('WIP Status Functionality', () => {
    it('TC283_WIPStatusWOHover', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','Wip Status')
        
        cy.get('[data-cy=workordersummary-icon]').first().trigger('mouseover')

        cy.get('.tooltip-inner').each(($el, index, list$) => {

            if(!$el.text().includes('WorkOrder Item Number')){
                throw new Error("WorkOrder Item Number is Missing")
            }

            if(!$el.text().includes('Purchase Order / Line Number')){
                throw new Error("Purchase Order / Line Number is Missing")
            }

            if(!$el.text().includes('Serial Number')){
                throw new Error("Serial Number is Missing")
            }

            if(!$el.text().includes('Status')){
                throw new Error("Status")
            }

            if(!$el.text().includes('Assigned To')){
                throw new Error("Assigned To")
            }
                
            
        })

        cy.checkWebConsoleTracking();

    })
})