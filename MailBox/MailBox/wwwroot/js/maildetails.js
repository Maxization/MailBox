
function DownloadAttachment(filename, id) {
    $.ajax({
        url: '/api/mailapi/downloadattachment',
        type: "GET",
        data: { filename: filename, id: id },
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            xhr.responseJSON.errors.forEach(function (item, index) {
                alert(item.message);
            });
        },
        success: function (result) {
            const byteCharacters = atob(result.fileContents);
            const byteNumbers = new Array(byteCharacters.length);
            for (i = 0; i < byteCharacters.length; i++)
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            const byteArray = new Uint8Array(byteNumbers);

            const blob = new Blob([byteArray], { type: result.contentType });
            const blobUrl = URL.createObjectURL(blob);

            const link = document.createElement("a");
            link.href = blobUrl;
            link.download = result.fileDownloadName;

            document.body.appendChild(link);
            link.dispatchEvent(
                new MouseEvent('click', {
                    bubbles: true,
                    cancelable: true,
                    view: window
                })
            );
            document.body.removeChild(link);
        }
    });
}
