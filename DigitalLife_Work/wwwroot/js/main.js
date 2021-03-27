$(document).ready(function () {
    let table = null;
    let projectid = $("#project").val();
    let clientid = $("#client").val();
    let date = $("#datepicker").val();


    console.log("project")
    console.log("project baslangic value")
    console.log(projectid);
    console.log("client baslangic value")
    console.log(clientid);
    console.log("Datetime baslangic value")
    console.log(date)



    $(document).on("change", "#client", function () {

        clientid = $(this).val();
        console.log("client change value")

        console.log(clientid)
        $.ajax({
            url: '/Home/Filter/' + clientid,
            data: { clientid: clientid, projectid: projectid, date: date },
            method: "post",
            success: function (res) {
                console.log(res.data)
                console.log(res.data.length)
                $("tbody").empty();
                for (var i = 0; i < res.data.length; i++) {
                    table = `<tr data-id="${res.data[i].id}">
                                    <td>
                                        ${res.data[i].id}
                                    </td>
                                    <td>
                                        ${res.data[i].dateTime}
                                    </td>
                                    <td>
                                        ${res.data[i].client.name}
                                    </td>
                                    <td>
                                       ${res.data[i].netAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].taxAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].totalAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].project.name}
                                    </td>
                                    <td>
                                      ${res.data[i].note}
                                    </td>
                                    <td>
                                       ${res.data[i].status}
                                    </td>
                                    <td>
                                        <a asp-action="Edit" asp-route-id="@item.Id" class="btn text-primary"><i class="fas fa-fw fa-pencil-alt"></i></a>

                                    </td>
                                </tr>`
                    $("tbody").append(table);
                }
            },
            error: function (err) {
                console.log(err.fail)
            }
        })
    })


    $(document).on("change", "#project", function () {
        console.log("project js clientid")
        console.log(clientid);
        projectid = $(this).val();
        console.log(projectid);

        $.ajax({
            url: '/Home/Filter/' + projectid,
            data: { clientid: clientid, projectid: projectid, date: date},
            method: "post",
            success: function (res) {
                console.log("get project success")
                console.log(res.data)
                console.log(res.data.length)
                $("tbody").empty();
                for (var i = 0; i < res.data.length; i++) {
                    table = `<tr data-id="${res.data[i].id}">
                                    <td>
                                        ${res.data[i].id}
                                    </td>
                                    <td>
                                        ${res.data[i].dateTime}
                                    </td>
                                    <td>
                                        ${res.data[i].client.name}
                                    </td>
                                    <td>
                                       ${res.data[i].netAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].taxAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].totalAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].project.name}
                                    </td>
                                    <td>
                                      ${res.data[i].note}
                                    </td>
                                    <td>
                                       ${res.data[i].status}
                                    </td>
                                    <td>
                                        <a asp-action="Edit" asp-route-id="@item.Id" class="btn text-primary"><i class="fas fa-fw fa-pencil-alt"></i></a>

                                    </td>
                                </tr>`
                    $("tbody").append(table);
                }

            },
            error: function (err) {
                console.log(err.fail)
            }
        })
    })


    $(document).on("change", "#datepicker", function () {

        console.log("datepicker js clientid")
        console.log(clientid);
        date = $("#datepicker").val();

        //let datetime = $("#datepicker").val().split('-');

        //let date = `${datetime[2]}/${datetime[1]}/${datetime[0]} `
        console.log(date);

        $.ajax({
            url: '/Home/Filter/' + date,
            data: { clientid: clientid, projectid: projectid, date: date },
            method: "post",
            success: function (res) {
                console.log("get project success")
                console.log(res.data)
                console.log(res.data.length)
                $("tbody").empty();
                for (var i = 0; i < res.data.length; i++) {
                    table = `<tr data-id="${res.data[i].id}">
                                    <td>
                                        ${res.data[i].id}
                                    </td>
                                    <td>
                                        ${res.data[i].dateTime}
                                    </td>
                                    <td>
                                        ${res.data[i].client.name}
                                    </td>
                                    <td>
                                       ${res.data[i].netAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].taxAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].totalAmount}
                                    </td>
                                    <td>
                                       ${res.data[i].project.name}
                                    </td>
                                    <td>
                                      ${res.data[i].note}
                                    </td>
                                    <td>
                                       ${res.data[i].status}
                                    </td>
                                    <td>
                                        <a asp-action="Edit" asp-route-id="@item.Id" class="btn text-primary"><i class="fas fa-fw fa-pencil-alt"></i></a>

                                    </td>
                                </tr>`
                    $("tbody").append(table);
                }

            },
            error: function (err) {
                console.log(err.fail)
            }
        })


    })
})