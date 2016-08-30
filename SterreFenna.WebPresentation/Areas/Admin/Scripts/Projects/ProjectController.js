var ProjectController = function () {
    return {
        init: init,
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
}();