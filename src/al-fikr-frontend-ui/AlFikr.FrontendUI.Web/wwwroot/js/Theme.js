var themesTable;
$(document).ready(function () {
    themesTable = new DataTable('#themesTable', {
        layout: {
            bottomEnd: {
                paging: {
                    boundaryNumbers: false
                }
            }
        }
    });
});

function deleteTheme(e) {
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
            $("#deleteTheme").submit();
        }
    });
}