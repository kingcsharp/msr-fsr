
// Define the tour!
    var tour = {
      id: "master-user-tour",
      steps: [
        {
          title: "Site Header",
          content: "This is the header of of the portal.",
          target: document.querySelector(".navbar-right"),
          placement: "bottom",
          xOffset:-100,
          yOffset:-20,
          arrowOffset: 230
        },
        {
          title: "Sidebar Menu",
          content: "This is the sidebar menu. The items you see herer are based on your role.",
          target: document.querySelector(".side-menu"),
          placement: "right",
          yOffset: 200
        },
        {
          title: "Sidebar Toggle Button",
          content: "This is the toggle button to show and hide the Sidebar. Click it to give it a try.",
          target: document.querySelector(".navbar-expand-toggle"),
          placement: "bottom"
        },
        {
          title: "Help Button",
          content: "This is the Help button which displays additional help for this screen. Click it to give it a try.",
          target: document.querySelector(".help-btn"),
          placement: "left"
        },
        {
          title: "Column Sorting",
          content: "Click any column header to sort by that row and the up and down arrows to change the sort order for that column.",
          target: document.querySelector(".ui-th-column"),
          placement: "top"
        },
        {
          title: "Column Options",
          content: "Click the colum options icon for additional options such as adding and removing columns from the grid, and more!",
          target: document.querySelector(".colmenu"),
          placement: "top"
        }
      ]
    };
