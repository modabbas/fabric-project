class CustomerOrderDetailMachine {

    initializeView() {
        this.AddCustomerOrderDetailMachineModel = $('#AddCustomerOrderDetailMachineModel');
        this.addFormCustomerOrderDetailMachine = this.AddCustomerOrderDetailMachineModel.find('#addFormCustomerOrderDetailMachine');
        this.orderdetailcustomer = this.addFormCustomerOrderDetailMachine.find('#orderdetailcustomer');
        this.machine = this.addFormCustomerOrderDetailMachine.find('#machine');
        this.amountwater = this.addFormCustomerOrderDetailMachine.find('#amountwater');
        this.AddCustomerOrderDetailMachineBtn = this.addFormCustomerOrderDetailMachine.find('.add-customerorderdetailmachine');
        return this;
    }

    bindEvents() {

        this.AddCustomerOrderDetailMachineBtn.click((event) => {
            const url = '/CustomerOrderDetailsMachine/SetCustomerOrderDetailMachine'
            this.AddForm(url);
            return false;

        });

        $(document).on("click", ".delete-customerorderdetailmachine", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deletecustomerorderdetailmachine(removeCard);
            return false;
        });

        $(document).on('click', '.edit-customerorderdetailmachine', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const OrderDetailCustomer = this.selectedCard.data('orderdetailcustomer');
            const Machine = this.selectedCard.data('machine');
            const AmountWater = this.selectedCard.data('amountwater');
            console.log(Id);
            $('#Id').val(Id);
            this.name.val(Name);
            this.amountwater.val(AmountWater);
            this.orderdetailcustomer.val(OrderDetailCustomer);
            this.machine.val(Machine);
            this.AddCustomerOrderDetailMachineModel.modal('show');
        })

        $(document).on('click', '.add-customerorderdetailmachine', (e) => {
            this.addFormCustomerOrderDetailMachine.get(0).reset();
            this.AddCustomerOrderDetailMachineModel.modal('show');
        });
    }


    deletecustomerorderdetailmachine(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف الطلبية للماكينة الموافقة ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitdeleteordermachine(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف الطلبية من الماكينة',
                    'success'
                )
            }
        });
    }



    submitdeleteordermachine(removedCard) {
        const removedOrderId = removedCard.data("id");
        $.ajax({
            url: `/CustomerOrderDetailsMachine/RemoveCustomerOrderDetailMachine/${removedOrderId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let CustomerOrderDetailMachineTable = $('#CustomerOrderDetailMachineTable').DataTable();
            CustomerOrderDetailMachineTable.row($('.id-' + removedOrderId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    AddForm(url) {
        var formOrderCustomerDetailMachine = new FormData(this.addFormCustomerOrderDetailMachine[0]);
        $.ajax({
            url: url,
            data: formOrderCustomerDetailMachine,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            $('.close').click();
            $('body').css({
                padding: 0
            })

            if (json.isAdd === true) {  // Add Form
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم إضافة طلبية لماكينة جديدة بنجاح ',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.AddRow(json);
                this.addFormCustomerOrderDetailMachine.get(0).reset();
            }
            else  // Edit Form
            {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم تعديل معلومات  بنجاح',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.selectedCard.data('amountwater', json.amountwater);
                this.selectedCard.data('machine', json.machine);
                this.selectedCard.data('orderdetailcustomer', json.orderdetailcustomer);


                this.selectedCard.find('.amountwater').html(json.amountwater);
                this.selectedCard.find('.machine').html(json.machine);
                this.selectedCard.find('.orderdetailcustomer').html(json.orderdetailcustomer);

            }

        })
    }

    AddRow(json) {
        var CustomerOrderDetailMachineTable = $('#CustomerOrderDetailMachineTable').DataTable();
        CustomerOrderDetailMachineTable.row.add($(` <tr data-id="${json.Id}" data-amountwater="${json.amountwater}"
					        data-orderdetailcustomer="${json.orderdetailcustomer}"  data-machine="${json.machine}"
									  role="row" class="odd id-${json.Id}">
                                    <td>${json.Id}</td>
                                      <td class="phone">@ordermachine.CustomerOrderDetailsId</td>
                                      <td class="email">@ordermachine.MachineId</td>
                                      <td class="email">@ordermachine.AmountWater</td>
                                      <td><a class="feather icon-edit-1 edit-customerorderdetailmachine"></a></td>
                                      <td><a class="btn trash delete-customerorderdetailmachine" style="color:red"><i class="fa fa-remove"></i></a></td>
                                 </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new CustomerOrderDetailMachine()

        .initializeView()
        .bindEvents();
})