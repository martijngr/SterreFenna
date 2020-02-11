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
        Loader.show();

        PopupService.setTitle("Serie verwijderen");
        PopupService.setBody("Weet je zeker dat je de serie wilt verwijderen?");
        PopupService.setConfirmButtonText("Verwijder serie");
        PopupService.confirmationButtonIsDeleteButton();
        PopupService.onConfirm(function () {
            HtmlFormBuilder.create("/Admin/Serie/Delete");

            HtmlFormBuilder.appendHiddenField("id", _serieId);
            HtmlFormBuilder.submit();
        });
        PopupService.show();
    }

    function submitAddSerie() {
        var validationErrors = SerieValidator.isFormValid();

        if (validationErrors.isValid()) {
            Loader.show();
            SerieFormElements.createButton.hide();
            if (_serieId > 0)
                SerieFormElements.deleteButton.hide();
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
                    Loader.hide();
                    //document.location.href = "/Admin/";
                });

                var myDropzone = this;

                // read all images from html and add them to this dropzone instance.
                var files = $("#sortable img").map(function () {
                    var location = $(this).attr('src');

                    var mockFile = {
                        name: "myimage.jpg",
                        size: 12345,
                        type: 'image/jpg',
                        status: Dropzone.QUEUED,
                        url: '/Series/96_My%20brother%20and%20I/P2080207%20%208-2-2020%20Webversie.JPG',
                        accepted: true
                    };

                    //loadXHR(location, function (blobImage) {
                    //    blobImage.name = "mock.jpg";
                    //    blobImage.url = '/Series/96_My%20brother%20and%20I/P2080207%20%208-2-2020%20Webversie.JPG';
                    //    blobImage.accepted = true;

                    //    myDropzone.emit("addedfile", blobImage);
                    //    //myDropzone.emit("thumbnail", blobImage, location);
                    //    myDropzone.files.push(blobImage);
                    //});
                   

                    //myDropzone.emit("addedfile", mockFile);
                    //myDropzone.emit("thumbnail", mockFile, location);
                    //myDropzone.files.push(mockFile);

                    //return mockFile;

                    // Call the default addedfile event handler
                    //myDropzone.emit("addedfile", mockFile);

                    //myDropzone.createThumbnailFromUrl(mockFile, location);
                    //myDropzone.emit("success", mockFile);
                    //myDropzone.emit("complete", mockFile);
                    
                    //myDropzone.files.push(mockFile);
                });
                //debugger;
                //myDropzone.uploadFiles(files);

                _initialize = false;
            }
        };
        Dropzone.autoDiscover = true;
        Dropzone.options.fooBar = dropzoneOptions;
    }

    function loadXHR(url, callback) {
        
        var xhr = new XMLHttpRequest();
        xhr.responseType = "blob";
        xhr.open("GET", url);
        xhr.onerror = function () {
            console.log("Network error.")
        };
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                callback(xhr.response);
            }
        }
        //xhr.onload = function () {
        //    if (xhr.status === 200) {
        //        var resp = xhr.response;
        //        console.log(resp);

        //        return xhr.response;
        //    }
        //    else {
        //        console.log("Loading error:" + xhr.statusText)
        //    }
        //};
        xhr.send();
            
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