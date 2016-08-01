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
            plugins: "columns, fullscreen, autoresize",
            toolbar: "columns, | bold, italic, underline, strikethrough, | alignleft, aligncenter, alignright, alignjustify, | styleselect, formatselect, fontsizeselect, | cut, copy, paste, | bullist, numlist, | outdent, indent, | blockquote, | undo, redo, | subscript, superscript",
            autoresize_max_height: 250,
        });
    }
}();