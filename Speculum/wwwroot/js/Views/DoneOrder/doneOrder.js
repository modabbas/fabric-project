//class DoneOrder {

//    initializeView() {
      
//    }

//    bindEvents() {
//        $(document).on("click", ".delivery-repoistory", (event) => {
//            event.preventDefault();
//            const removeCard = $(event.target).closest("tr");
//            this.DoneOrder(removeCard);
//            return false;
//        });
//    }

//    DoneOrder(removedCard) {
//        const swalWithBootstrapButtons = Swal.mixin({
//            customClass: {
//                confirmButton: 'btn btn-success',
//                cancelButton: 'btn btn-danger'
//            },
//            buttonsStyling: true
//        });
//        swalWithBootstrapButtons.fire({
//            title: "هل أنت متأكد؟",
//            text: "هل فعلاً تريد نقل هذه الطلبية الى مستودع التسليم ؟",
//            icon: "warning",
//            showCancelButton: true,
//            confirmButtonText: 'نعم!',
//            cancelButtonText: 'إلغاء',
//            reverseButtons: false

//        }).then((result) => {
//            if (result.value) {
//                this.submitDeleteColor(removedCard);
//                swalWithBootstrapButtons.fire(
//                    'تم!',
//                    'لقد تم نقل الطلبية الى مستودع التسليم',
//                    'success'
//                )
//            }
//        });
//    }

//    submitDeleteColor(removedCard) {
//        const removedColorId = removedCard.data("id");
//        $.ajax({
//            url: `/Color/RemoveColor/${removedColorId}`,
//            type: "Delete",
//            dataType: "json"
//        }).done((json) => {
//            let ColorTable = $('#ColorTable').DataTable();
//            ColorTable.row($('.id-' + removedColorId)).remove().draw(false);
//        }).fail((xhr, status, errorThrown) => {

//        }).always((xhr, status) => {

//        });
//    }

//}



//$(document).ready(() => {
//    new DoneOrder()
//        .initializeView()
//        .bindEvents();
//})