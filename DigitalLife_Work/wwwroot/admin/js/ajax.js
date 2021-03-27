$(function () {
    $(".table-shorting tbody").sortable({
        cursor: "move",
        placeholder: "sortable-placeholder",
        helper: function (e, tr) {
            var $originals = tr.children();
            var $helper = tr.clone();
            console.log(tr.children());
            $helper.children().each(function (index) {
                $(this).width($originals.eq(index).width());
            });
            return $helper;
        }
    }).disableSelection();
    $('.confirmqueue').click(function (e) {
        let tablename = $(this).attr('data-action');
        e.preventDefault();
        let tBody = $('.table-shorting ').children("tbody");
        var orderArray = [];
        for (var i = 0; i < $(tBody).children("tr").length; i++) {
            orderArray.push(new row($(tBody).children("tr")[i].getAttribute("data-id"), i));
        }
        console.log(orderArray);
        $.ajax({
            url: '/webcms/ajaxs/order',
            type: "post",
            data: { order: orderArray, table: tablename },
            datatype: "json",
            success: function (res) {
                if (res.isSuccess) {
                    swal(res.messages, "", "success");
                    setTimeout(function () {
                        window.location.reload();
                    }, 1500)
                } else {
                    swal(res.messages, "", "error");
                }
            }
        });
        function row(id, place) {
            this.Id = id,
            this.Place = place
        }
    });
})

