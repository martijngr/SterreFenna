var SerieValidator = function () {
    return {
        isFormValid: isFormValid,
    };

    function isFormValid() {
        var validationErrors = new ValidationResult();

        if (!isExistingProjectSelected() && !isNewProjectNameProvided()) {
            validationErrors.add("Geen project opgegeven.");
        }

        if (!isNewSerieNameProvided()) {
            validationErrors.add("Geen naam voor de serie opgegeven.");
        }

        if (!anyItemsPresent()) {
            validationErrors.add("De serie bevat geen beeldmateriaal.");
        }

        return validationErrors;
    }

    function isExistingProjectSelected() {
        return SerieFormElements.projectDropdown.val() != "-1";
    }

    function isNewProjectNameProvided() {
        return SerieFormElements.newProjectName.val().length > 0;
    }

    function isNewSerieNameProvided() {
        return SerieFormElements.newSerieName.val().length > 0;
    }

    function anyItemsPresent() {
        return $("#sortable").children().length > 0;
    }
}();