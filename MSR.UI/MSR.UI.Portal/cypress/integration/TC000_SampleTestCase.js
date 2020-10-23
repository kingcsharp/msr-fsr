describe('Sample Test Case', () => {
    it('TC000_SampleTestCase', () => {

        var isLocalEnvironment = Cypress.env('environment')

        if (isLocalEnvironment === 'local') {


            /*
                In some scenarios you may want to mock an http request because it is not feasible send the request though the system.
                An example is testing the UI for reset password. To do this we call cy.server() to start a mock server. 
                Then we create an http request to mock, and we can even include mock data to be used by the app.
    
                WARNING: Cypress says this about the type of HTTP Requests it can mock
    
                "Please be aware that Cypress only currently supports intercepting XMLHttpRequests. Requests using the Fetch API and other 
                types of network requests like page loads and <script> tags will not be intercepted or visible in the Command Log."
            */
            cy.server()

            cy.route({
                method: 'POST',
                url: '/v1/User',
                response: []
            })


            /*
                We can get cypress environment variables and store them as local variables
                for use.
                Cypress environment variables are strored in cypress/config/[environment].json
                The environment is set when we run 'npm run cypress -env configFile=[environment]'
            */
            var adminUsername = Cypress.env('admin-username')
            var adminPassword = Cypress.env('admin-password')

            //Since we set the 'baseUrl' in our config file, we use '/' to navigate to the default page
            cy.visit('/')

            //To verify the url for the page, we use an include method.
            //This only works after a visit, if navigation is done via the app, we need to use a get method so cypress doesn't timeout
            cy.url().should('include', '/login')


            //After we create out window, we create two spies to check the development console for wanrnings and errors
            cy.window().then((win) => {

                cy.spy(win.console, 'error').as('errorMessage')
                cy.spy(win.console, 'warn').as('warningMessage')

            })

            //We use a get method to find the input element we want.
            //We use type to well type text into the input
            //We then verify the input has the information we typed using the should method and the 'have.value' option
            cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
            cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)

            //Here we use the click method to click out button
            cy.get('[data-cy=submit-button]').click()

            //Page navigations can sometimes take a while due to network connectivity, or the db and server being under heavy load.
            //We can set a custom timeout for getting an element as shown here. So far 20 seconds seems to always work regardless of system load
            cy.get('[data-cy=logout-link]', { timeout: 20000 }).url().should('include', '/people/people')

            //We can also search the DOM tree for a specific element containing text we use to distinguish a button or element.
            //We use the contains method for this
            cy.get('[data-cy=usertable-header]').contains('Id').click();

            //We can also use a find method to search the DOM tree
            cy.get('[data-cy=usergrid-options]').find('.gridDropdown').click()

            //You may need to scroll an element in the DOM, which you can use the ScrollTo method with different parameters to do this
            cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom')

            cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click()

            cy.get('[data-cy=usertable-header]').contains('Created On').click();

            //Here we fail the test if errors and warning have been called
            cy.get('@warningMessage').should("not.called")
            cy.get('@errorMessage').should("not.called") 

        }



    })
})