var collectionsTable;
$(document).ready(function () {
    collectionsTable = new DataTable('#collectionsTable', {
        layout: {
            bottomEnd: {
                paging: {
                    boundaryNumbers: false
                }
            }
        }
    });
});

function deleteCollection(e) {
    swal({
        title: 'êtes-vous sûr ?',
        text: 'Une fois supprimé, vous ne pourrez plus récupérer',
        icon: "warning",
        buttons:
        {
            cancel: {
                text: "Annuler",
                value: null,
                visible: true,
                closeModal: true,
            },
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        }
    }).then((willDelete) => {
        if (willDelete) {
            $("#deleteCollection").submit();
        }
    });
}