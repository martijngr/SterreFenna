tinymce.PluginManager.add('columns', function (editor, url) {
    // Add a button that opens a window
    editor.addButton('columns', {
        text: 'Maak kolommen',
        icon: false,
        onclick: function () {
            var contentWithoutPtag = editor.getContent().replace(/^\<p\>/, "").replace(/\<\/p\>$/, "");
            if (contentWithoutPtag.length > 70) {
                var content = contentWithoutPtag;
                var half = content.length / 2;
                //debugger;
                var index = indexOfSpace(content, half);
                var leftColumnContent = content.substring(0, index);
                var rightColumnContent = content.substring(index, content.length);

                rightColumnContent = rightColumnContent.indexOf(" ") == 0 ? rightColumnContent.replace(" ", "") : rightColumnContent;
                editor.setContent('<table><tr><td style="width:50%" valign="top">' + leftColumnContent + '</td><td style="width:50%" valign="top">' + rightColumnContent + '</td></tr></table>');
            }
        }
    });

    function indexOfSpace(value, currentIndex) {
        var spaceFound = false;

        while (!spaceFound) {

            if (currentIndex > value.length)
                break;

            spaceFound = value.charAt(currentIndex) == " ";

            if(!spaceFound)
                currentIndex++;
        }

        return currentIndex;
    }


    // Adds a menu item to the tools menu
    editor.addMenuItem('columns', {
        text: 'Example plugin',
        context: 'tools',
        onclick: function () {
            // Open window with a specific url
            editor.windowManager.open({
                title: 'TinyMCE site',
                url: 'http://www.tinymce.com',
                width: 800,
                height: 600,
                buttons: [{
                    text: 'Close',
                    onclick: 'close'
                }]
            });
        }
    });
});