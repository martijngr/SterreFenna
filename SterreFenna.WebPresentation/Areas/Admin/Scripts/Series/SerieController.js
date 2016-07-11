var SerieController = function () {
    return {
        init: init,
        onProjectDropdownChanged: onProjectDropdownChanged,
        getOrderOfFilenames: getOrderOfFilenames,
        removePhotoFromList: removePhotoFromList,
        getBase64String: getBase64String,
        submitAddSerie: submitAddSerie,
        addFileToSortablePics: addFileToSortablePics
    };

    function init() {
        $(function () {
            $("#sortable").sortable();
            $("#sortable").disableSelection();
        });

        var dropzoneOptions = {
            autoProcessQueue: false,
            uploadMultiple: true,
            addRemoveLinks: false,
            previewTemplate: '<div style="display:none"></div>',
            paramName: 'files',
            parallelUploads: 100,
            init: function () {
                this.on("sending", function (file, xhr, formData) {
                    formData.append('name', SerieFormElements.newSerieName.val());
                    formData.append('publicationDate', SerieFormElements.publicationDate.val());
                    formData.append('filenameOrder', SerieController.getOrderOfFilenames());
                    formData.append('newProjectName', SerieFormElements.newProjectName.val());
                    formData.append('projectId', SerieFormElements.projectDropdown.val());
                });

                this.on("addedfile", function (file) {
                    SerieController.addFileToSortablePics(file);
                });
            }
        };
        Dropzone.autoDiscover = true;
        Dropzone.options.fooBar = dropzoneOptions;
    }

    function onProjectDropdownChanged(dropdown) {
        var dropdownValue = $(dropdown).val();
        var projectTextbox = $("#newProjectName");

        if (dropdownValue == "-1") {
            projectTextbox.prop("disabled", false);
        }
        else {
            projectTextbox.prop("disabled", true);
        }
    }

    function getOrderOfFilenames() {
        var imageNames = [];
        var images = $("#sortable li img");

        $.each(images, function (index, image) {
            imageNames.push($(image).prop('alt'));
        });

        return imageNames;
    }

    function removePhotoFromList(element) {
        $(element).parent().remove();
    }

    function getBase64String(file, callback) {
        var reader = new FileReader();

        // Closure to capture the file information.
        reader.onload = function (e) {
            //get the base64 url
            var base64URL = e.target.result;

            if (callback != null)
                callback(base64URL);
        };

        // Read in the image file as a data URL.
        reader.readAsDataURL(file);
    }

    function submitAddSerie() {
        var validationErrors = SerieValidator.isFormValid();

        if (validationErrors.isValid()) {
            var dropzone = Dropzone.instances[0];
            dropzone.processQueue();
        }
        else {
            alert(validationErrors.getString());
        }
    }

    function addFileToSortablePics(file) {

        SerieController.getBase64String(file, function (imageString) {
            var container = $("#sortable");
            var image = '<img src="' + imageString + '" alt="' + file.name + '" title="' + file.name + '"/>';
            var trash = '<i class="fa fa-trash" onclick="SerieController.removePhotoFromList(this)"></i>';
            var item = '<li class="ui-state-default height-90">' + image + trash + '</li>';

            container.append(item);
        });
    }
}();