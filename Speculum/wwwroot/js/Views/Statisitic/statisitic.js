$(document).ready(function () {

    $.ajax({
        url: "/Statistics/Chart",
        type: "POST",
        dataType: "json",
        processData: false,
        contentType: false
    }).done((json) => {
        ColorChart(json.colors);
        MaterialChart(json.materials);
        CustomersOrders(json);
        NumberOrdersInMonth(json);
    });

});

function CustomersOrders(json) {
    var OrderCount = [];
    var CustomerName = [];
    OrderCount[0] = 0;
    CustomerName[0] = "";
    var SumAllOrders = 0;
    for (var i = 0; i < json.customersOrders.length; i++)
    {
        OrderCount[i+1] = json.customersOrders[i].orderCount;
        SumAllOrders += json.customersOrders[i].orderCount;
        CustomerName[i + 1] = json.customers[i].customerName;
    }
    const colors = [
        '#FFFFFF', '#21BAFF', '#3FFFAC', '#E98ABC', '#CF895A', '#FCA8F5', '#99B86F', '#3E7935',
        '#854ECB', '#E213EF', '#F1128C', '#E56A6A', '#DA885A', '#DECA5B', '#99B86F', '#5A8988', '#5B8988'

    ];
    new Chart(document.getElementById("CustomersOrdersChart"), {
        type: 'bar',
        data: {
            labels: CustomerName,
            datasets: [
                {
                    data: OrderCount,
                    backgroundColor: colors,
                    hoverBackgroundColor: 'rgba(100, 100, 100 , 1)',
                }
            ]
        },
        options: {
            legend: {
                display: false
            },

            title: {
                display: true,
                position: 'bottom',
                text: 'عدد الطلبيات الاجمالي : ' + SumAllOrders,
                fontSize: 20,
                fontColor: '#764ba2'
            }
        }
    });
}

function ColorChart(json) {
    console.log(json[0].name);
    ColorName = [];
    ColorAmount = []
    for (var i = 0; i < json.length; i++)
    {
        ColorName[i] = json[i].name;
        ColorAmount[i] = json[i].amount
    }
    const colors = [
        '#EE481F', '#CA9B0E', '#DFF10B', '#92AA2F', '#DA885A', '#DECA5B', '#99B86F', '#3E7935',
        '#5A8988', '#E213EF', '#F1128C', '#E56A6A', '#DA885A', '#DECA5B', '#99B86F', '#3E7935', '#5A8988'

    ];
    new Chart(document.getElementById("ColorChart"), {
        type: 'pie',
        data: {
            labels: ColorName,
            datasets: [
                {
                    data: ColorAmount,
                    backgroundColor: colors,
                    hoverBackgroundColor: 'rgba(100, 100, 100 , 1)',
                }
            ]
        },
        options: {
            legend: {
                display: false
            },

            title: {
                display: true,
                position: 'bottom',
                text: 'عدد الألوان الاجمالي : ' + json.length,
                fontSize: 20,
                fontColor: '#764ba2'
            }
        }
    });
}

function MaterialChart(json) {
    MaterialName = [];
    MaterialAmount = [];
    for (var i = 0; i < json.length; i++)
    {
        MaterialName[i] = json[i].name;
        MaterialAmount[i] = json[i].amount;
    }
    const colors = [
        '#EE9814', '#DA0B0B', '#DEE10F', '#87BA2E', '#30D719', '#38C67D', '#35DBC7', '#299EE5',
        '#854ECB', '#E213EF', '#F1128C', '#E56A6A', '#DA885A', '#DECA5B', '#99B86F', '#3E7935', '#5A8988'

    ];
    new Chart(document.getElementById("MaterialChart"), {
        type: 'doughnut',
        data: {
            labels: MaterialName,
            datasets: [
                {
                    data: MaterialAmount,
                    backgroundColor: colors,
                    hoverBackgroundColor: 'rgba(100, 100, 100 , 1)',
                }
            ]
        },
        options: {
            legend: {
                display: false
            },

            title: {
                display: true,
                position: 'bottom',
                text: 'عدد لمواد الاجمالي : ' + json.length,
                fontSize: 20,
                fontColor: '#764ba2'
            }
        }
    });
}

function NumberOrdersInMonth(json) {
    const colors = [
        '#EE9814', '#DA0B0B', '#DEE10F', '#87BA2E', '#30D719', '#38C67D', '#35DBC7', '#299EE5',
        '#854ECB', '#E213EF', '#F1128C', '#E56A6A', '#DA885A', '#DECA5B', '#99B86F', '#3E7935', '#5A8988'

    ];
    new Chart(document.getElementById("NumberOrdersInMonthChart"), {
        type: 'bar',
        data: {
            labels: /*json.categoryName*/['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [
                {
                    data: json.countOrderOfMonth,
                    backgroundColor: colors,
                    hoverBackgroundColor: 'rgba(100, 100, 100 , 1)',
                }
            ]
        },
        options: {
            legend: {
                display: false
            },
        }
    });
}



