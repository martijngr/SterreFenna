var ProjectController = function () {
    return {
        init: init,
        deleteProject: deleteProject,
    };

    function init(description) {
        initializeDescriptionWithTinyMce();
    }

    function initializeDescriptionWithTinyMce() {
        tinymce.init({
            selector: '#description',
            plugins: "columns, fullscreen, autoresize, paste, table, code",
            toolbar: "columns, table, | bold, italic, underline, strikethrough, | alignleft, aligncenter, alignright, alignjustify, | styleselect, formatselect, fontsizeselect, | cut, copy, paste, | bullist, numlist, | outdent, indent, | blockquote, | undo, redo, | subscript, superscript, | code",
            paste_as_text: true,
            autoresize_max_height: 250,
        });
    }

    function deleteProject() {
        Loader.show();
        ProjectFormElements.buttonBarButtons.hide();

        PopupService.setTitle("Project verwijderen");
        PopupService.setBody("Weet je zeker dat je het project en alle onderliggende series wilt verwijderen?");
        PopupService.setConfirmButtonText("Verwijder project");
        PopupService.confirmationButtonIsDeleteButton();
        PopupService.onConfirm(function () {
            HtmlFormBuilder.create("/Admin/Project/Delete");
            HtmlFormBuilder.appendHiddenField("id", ProjectFormElements.projectId);

            HtmlFormBuilder.submit();
        });
        PopupService.show();
    }
}();