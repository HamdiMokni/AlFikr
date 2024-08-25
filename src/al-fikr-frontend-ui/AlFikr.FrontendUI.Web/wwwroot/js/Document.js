async function sequentialDownloads() {

    var file = document.getElementById('attachmentFileDocument').files[0];

    var blob = file;
    var BYTES_PER_CHUNK = 1024 * 1024 * 1;
    var SIZE = file.size;
    var start = 0;
    var end = BYTES_PER_CHUNK;
    var count = SIZE % BYTES_PER_CHUNK == 0 ? SIZE / BYTES_PER_CHUNK : Math.floor(SIZE / BYTES_PER_CHUNK) + 1;
    var index = 0;

    while (start < SIZE) {
        var chunk = blob.slice(start, end);
        var formData = new FormData();
        formData.append("FILE_NAME", file.name.slice(0, file.name.length - 4));
        formData.append("CHUNK_INDEX", index);
        formData.append("CHUNK", chunk);

        start = end;
        end = start + BYTES_PER_CHUNK;

        var a = await RequestSendContentPromise(formData);

        index++;
    }

    if (index === count) {
        await $.ajax({
            url: '/Document/UploadComplete?FILE_NAME=' + file.name.slice(0, file.name.length - 4),
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            method: 'POST',
            success: function (resp) {

            },
            error: function (xhr, error, status) {
                console.log(error);
            }
        })
        $('.loader').hide();
    }
};

async function RequestSendContentPromise(el) {

    await $.ajax({
        url: '/Document/UploadChunks',
        type: 'POST',
        data: el,
        cache: false,
        contentType: false,
        processData: false,
        method: 'POST',
        success: function (data) {

        }
    });

    return "OK";
}

async function upsertDocument() {

    $('.loader').show();

    if ($("#myForm")[0].reportValidity()) {
        if (validateprice() == true) {

            var file = document.getElementById('attachmentFileDocument').files[0];
            var coverPage = document.getElementById('attachmentCoverPage').files[0];
            var element = document.getElementById('attachmentFileDocumentName');
            var element2 = document.getElementById('attachmentCoverPageName');

            if (file != null && file != undefined) {
                element.value = file.name;
                await sequentialDownloads();
            }

            if (coverPage != null && coverPage != undefined) {
                element2.value = coverPage.name;
                await sequentialDownloadsCoverPage();
            }

            $("#attachmentFileDocument").val(null);
            $("#attachmentCoverPage").val(null);

            var a = Upsert();

            a.then(function (data) {
                window.location.href = data.message;
            });
        }
        else {
            $('.loader').hide();

            iziToast.error({
                title: (culture == 'ar') ? 'الرجاء التثبت من الثمن' : (culture == 'en') ? 'Please verify the price' : (culture == 'es') ? 'Por favor verifique el precio' : 'Veuillez vérifier le prix',
                position: 'topRight'
            });
        }
    }
    else {
        $('.loader').hide();
    }
}


async function sequentialDownloadsCoverPage() {

    var file = document.getElementById('attachmentCoverPage').files[0];

    var blob = file;
    var BYTES_PER_CHUNK = 1024 * 1024 * 1;
    var SIZE = file.size;
    var start = 0;
    var end = BYTES_PER_CHUNK;
    var count = SIZE % BYTES_PER_CHUNK == 0 ? SIZE / BYTES_PER_CHUNK : Math.floor(SIZE / BYTES_PER_CHUNK) + 1;
    var index = 0;

    while (start < SIZE) {
        var chunk = blob.slice(start, end);
        var formData = new FormData();
        formData.append("FILE_NAME", file.name.slice(0, file.name.length - 4));
        formData.append("CHUNK_INDEX", index);
        formData.append("CHUNK", chunk);

        start = end;
        end = start + BYTES_PER_CHUNK;

        var a = await RequestSendContentPromiseCoverPage(formData);

        index++;
    }

    if (index === count) {
        await $.ajax({
            url: '/Document/UploadCoverPageComplete?FILE_NAME=' + file.name.slice(0, file.name.length - 4),
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            method: 'POST',
            success: function (resp) {

            },
            error: function (xhr, error, status) {
                console.log(error);
            }
        })
    }
};

async function RequestSendContentPromiseCoverPage(el) {
    await $.ajax({
        url: '/Document/UploadChunksCoverPage',
        type: 'POST',
        data: el,
        cache: false,
        contentType: false,
        processData: false,
        method: 'POST',
        success: function (data) {

        }
    });
    return "OK";
}