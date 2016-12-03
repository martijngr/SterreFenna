var HtmlFormBuilder = function (action) {
    var _form;

    return {
        create, create,
        appendHiddenField: appendHiddenField,
        getHtmlForm: getHtmlForm,
        submit: submit,
    };

    function create(action) {
        _form = document.createElement("form");
        _form.setAttribute("method", "post");
        _form.setAttribute("action", action);
    }

    function appendHiddenField(name, value) {
        var hiddenField = document.createElement("input");
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("name", name);
        hiddenField.setAttribute("value", value);

        _form.appendChild(hiddenField);
    }

    function getHtmlForm() {
        return _form;
    }

    function submit() {
        document.body.appendChild(_form);
        _form.submit();
    }
}();