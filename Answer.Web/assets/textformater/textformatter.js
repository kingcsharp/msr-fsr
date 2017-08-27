$(document).ready(function () {
    resetTextAdder();
    onLoadFunction();
    startLogOutTimer();
    showRowActionItems();
    // initialiseWebWizRTE();

    $('.text-formatter')
        .dblclick(storeCaret(this))
        .select(storeCaret(this))
        .click(storeCaret(this))
        .keyup(storeCaret(this))
        .mouseup(storeCaret(this))
        .blur(checkMaxSize(this, 4000));
});
