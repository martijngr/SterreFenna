var SerieController = function () {
    return {
        init: init,
        onProjectDropdownChanged: onProjectDropdownChanged,
        getOrderOfFilenames: getOrderOfFilenames,
        removePhotoFromList: removePhotoFromList,
        getBase64String: getBase64String,
        submitAddSerie: submitAddSerie,
        addFileToSortablePics: addFileToSortablePics,
        markAsFavourite: markAsFavourite,
        deleteSerie: deleteSerie,
    };

    var _serieId;
    var _favouriteItems;
    var _initialize;
    var _progressTemplate;

    function init(serieId, favouriteItems) {
        _serieId = serieId || 0;
        _favouriteItems = favouriteItems || [];
        _initialize = true;
        _progressTemplate = "";

        Loader.hide();

        handleProjectNameVisibility();

        setupDropzone();
        setupDragArea();

        loadProgressTemplate();
    }

    function onProjectDropdownChanged(dropdown) {
        handleProjectNameVisibility();
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

    function deleteSerie() {
        HtmlFormBuilder.create("/Admin/Serie/Delete");

        HtmlFormBuilder.appendHiddenField("id", _serieId);
        HtmlFormBuilder.submit();
    }

    function submitAddSerie() {
        var validationErrors = SerieValidator.isFormValid();

        if (validationErrors.isValid()) {
            Loader.show();
            SerieFormElements.createButton.hide();
            SerieFormelements.deleteButton.hide();
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
            var favourite = '<i class="fa fa-asterisk" onclick="SerieController.markAsFavourite(this)"></i>';
            var item = '<li class="ui-state-default height-90">' + image + trash + favourite + '</li>';

            container.append(item);
        });
    }

    function markAsFavourite(element) {
        var src = $(element).parent().find('img').attr('src');
        var filename = getFilename(src);
        var index = _favouriteItems.indexOf(filename);

        if (index > -1) {
            _favouriteItems.splice(index, 1);

            $(element).removeClass('favourite-item');
        }
        else {
            _favouriteItems.push(filename);

            $(element).addClass('favourite-item');
        }
    }

    function handleProjectNameVisibility() {
        var dropdownValue = SerieFormElements.projectDropdown.val();
        var projectTextbox = SerieFormElements.newProjectName;

        if (dropdownValue == "-1") {
            projectTextbox.prop("disabled", false);
        }
        else {
            projectTextbox.prop("disabled", true);
        }
    }

    function setupDropzone() {
        var dropzoneOptions = {
            autoProcessQueue: false,
            uploadMultiple: true,
            addRemoveLinks: false,
            previewTemplate: '<div style="display:none"></div>',
            paramName: 'files',
            parallelUploads: 100,
            uploadprogress: function (file, progress, bytesSent) {
                console.log(file.name, progress, bytesSent);

                handleProgress(file.name, progress + "%");
            },
            init: function () {
                this.on("addedfile", function (file) {
                    if (_initialize)
                        return;

                    SerieController.addFileToSortablePics(file);
                });

                this.on("sending", function (file, xhr, formData) {
                    if (_serieId > 0)
                        formData.append('serieId', _serieId);

                    formData.append('name', SerieFormElements.newSerieName.val());
                    formData.append('publicationDate', SerieFormElements.publicationDate.val());
                    formData.append('filenameOrder', SerieController.getOrderOfFilenames());
                    formData.append('favouriteFilenames', _favouriteItems);
                    formData.append('newProjectName', SerieFormElements.newProjectName.val());
                    formData.append('projectId', SerieFormElements.projectDropdown.val());
                    formData.append('credits', SerieFormElements.credits.val());
                });

                this.on("queuecomplete", function (file) {
                    document.location.href = "/Admin/";
                });

                var myDropzone = this;
                var images = $("#sortable img").map(function () {
                    var location = $(this).attr('src');

                    var mockFile = {
                        name: "myimage.jpg",
                        size: 12345,
                        type: 'image/jpeg',
                        status: Dropzone.QUEUED,
                        url: location
                    };

                    // Call the default addedfile event handler
                    myDropzone.emit("addedfile", mockFile);

                    myDropzone.files.push(mockFile);
                });

                _initialize = false;
            }
        };
        Dropzone.autoDiscover = true;
        Dropzone.options.fooBar = dropzoneOptions;
    }

    function setupDragArea() {
        $("#sortable").sortable();
        $("#sortable").disableSelection();
    }

    function getFilename(value) {
        var lastIndex = value.lastIndexOf("/");
        var filename = value.substring(lastIndex + 1, value.length);

        return filename;
    }

    function handleProgress(fileName, progressValue) {
        var liElement = getImgElementByAltTag(fileName).parent();
        var progressElement = liElement.find("div[data='" + fileName + "']");

        if (progressElement.length == 0) {
            var newProgressTemplate = _progressTemplate.clone();
            newProgressTemplate.attr("data", fileName);
            setProgress(newProgressTemplate, progressValue);
            liElement.append(newProgressTemplate);
        }
        else {
            setProgress(progressElement, progressValue);
        }
    }

    function setProgress(progressElement, progressValue) {
        progressElement.find(".progress-value").first().css("width", progressValue);
        progressElement.find(".progress-value-text").first().text(progressValue);
    }

    function getImgElementByAltTag(value) {
        return $("img[title='" + value + "']");
    }

    function loadProgressTemplate() {
        $.get("/Areas/Admin/Views/Serie/UploadProgressbar.html", function (response) {
            _progressTemplate = $(response);
        });
    }
}();