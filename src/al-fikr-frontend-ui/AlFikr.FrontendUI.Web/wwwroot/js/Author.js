var authorTable;
$(document).ready(function () {
    authorTable = new DataTable('#authorsTable', {
        layout: {
            bottomEnd: {
                paging: {
                    boundaryNumbers: false
                }
            }
        }
    });
});

function deleteAuthor(e) {
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
            $("#deleteAuthor").submit();
        }
    });

}