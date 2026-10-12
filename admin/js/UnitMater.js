
    var arrayOfUnit = [];
var Count = 1;
$(document).ready(function () {

    GetProduct();
    GetCountry();
});

function GetCountry() {
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetClintCountry",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {

            var innerHTML = "";
            innerHTML += "<option  value = ''>" + "Select Country" + "</option>";
            $(data.DataList).each(function (index, value) {
                if (value.CountryName != null || value.CountryName != "" || value.CountryName != undefined) {
                    innerHTML += "<option class='option01' value = '" + value.CountryID + "'> " + value.CountryName + "</option>"
                }
            });

            $('#CountryFID').html(innerHTML);
            $('#CountryFID').selectpicker('refresh');

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function GetProduct() {
    $.ajax({
        type: "POST",
        url: "/AdminChemicalPricing/GetProduct",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {

            var innerHTML = "";
            innerHTML += "<option  value = ''>" + "Select ProductName" + "</option>";
            $(data.DateList).each(function (index, value) {
                if (value.ProductName != null || value.ProductName != "" || value.ProductName != undefined) {
                    innerHTML += "<option class='option01' value = '" + value.ID + "'> " + value.ProductName + "</option>"
                }
            });

            $('#Product').html(innerHTML);
            $('#Product').selectpicker('refresh');

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
$('#CountryFID').on('change', function () {
    
    GetProduct();
    $("#Product").val('');
});

function PushUnit() {
    
        if (arrayOfUnit.length > 0) {
            arrayOfUnit = [];
            $('.tbodydata').html('');
            $('.tbodydata').html('');
        }

        GetavilableProductCountryUnit();
        var CountryFID = $("#CountryFID").val();
        var CountryName = $("#CountryFID option:selected").text();
        CountryName = CountryName.replace(/^\s+/, '');
        var Product = $("#Product").val();
        var ProductName = $("#Product option:selected").text();
       
        if (CountryFID== "") {
           swal("Warning", "Please Select Country", "error");
           return false;
        }
        else if (Product == "") {
        swal("Warning", "Please Select Product", "error");
         return false;
        }
      
        else {
            var arrayOfUnitdata = "";
            if (CountryName == "India") {
                arrayOfUnitdata = [
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "INR/Tonne", Currency: "INR" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "USD/Tonne", Currency: "USD" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "YEN/Tonne", Currency: "YEN" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "EURO/Tonne", Currency: "EURO" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "WON/Tonne", Currency: "WON" }
                ];
            }
            else {
                arrayOfUnitdata = [
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "USD/Tonne", Currency: "USD" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "YEN/Tonne", Currency: "YEN" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "EURO/Tonne", Currency: "EURO" },
                    { ProductName: ProductName, CountryName: CountryName, UnitName: "WON/Tonne", Currency: "WON" }
                ];
            }
          

          
            $('#dvTable').css('display', 'block');
            var tbodyHTML = '';

            arrayOfUnitdata.forEach(row => {
                Count++;
            arrayOfUnit.push({
                Id: "row" + Count,
                Product: Product,
                UnitName: row.UnitName,
                country: CountryFID,
                CountryName: row.CountryName,
                Currency: row.Currency
                });

            tbodyHTML += "<tr id='row" + Count + "'>";
            tbodyHTML += "<td >" + row.CountryName + "</td>";
            tbodyHTML += "<td>" + row.ProductName + "</td>";
            tbodyHTML += "<td>" + row.UnitName + "</td>";
            tbodyHTML += "<td>" + row.Currency + "</td>";
            tbodyHTML += "<td><button class='btn btn-danger' id='remove'>" + "Remove" + "</button></td>";
            tbodyHTML += "</tr>"

            });
       
        $('.tbodydata').append(tbodyHTML);

        } 
       
    }

    function saveunit()
    {
        
    $.ajax({
        type: "POST",
            url: "/AdminChemicalPricing/InsertUnit",
            data: JSON.stringify(arrayOfUnit),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

        $(response).each(function (index, value) {

            if (value.ValidationMessage == "Success") {
                swal({ title: "Success!", type: "success" }).then(function () {
                    window.location.reload();
                });
             
            }
            else if (value.ValidationMessage == "Already Exists") {
                swal({ title: "Already Exists", type: " Already Exists" }).then(function () {
                   // window.location.reload();
                });
              
            }

            else {
                swal({ title: "Failed!", type: "success" }).then(function () { });
            }

        });

    },
            failure: function (response) {
        console.log(response)
    },
            error: function (response) {
        console.log(response)
    }
    });
    }

    $("#Unitlist").on("click", "#remove", function () {
        const rowId = $(this).closest('tr').attr('id');

        // Remove from DOM
        $(this).closest('tr').remove();

        // Remove from array
        rowArray = rowArray.filter(row => row.RowId !== rowId);
    });

    function GetavilableProductCountryUnit() {
        
        var CountryName = $("#CountryFID option:selected").text();
        CountryName = CountryName.replace(/^\s+/, '');
    var Product = $("#Product").val();
    $.ajax({
        type: "POST",
        url: "/AdminChemicalPricing/Getavilability_ProductCountryUnit",
        data: {CountryName: CountryName, Product: Product},
      //  contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (json) {
            
            var tbodyHTML = '';
            if (json != null) {
                $(json).each(function (idx, elm) {
                    
                    tbodyHTML += "<tr>";
                    tbodyHTML += "<td>" + elm.CountryName + "</td>";
                    tbodyHTML += "<td>" + elm.ProductName + "</td>";
                    tbodyHTML += "<td>" + elm.Currency + "</td>";
                    tbodyHTML += "<td>" + elm.UnitName + "</td>";
                    tbodyHTML += "</tr>"
                });
            }
            else {
                tbodyHTML = "<tr><td class='text-center' colspan='20'><span>No records found..!!</span> </td></tr>";
            }
            $('.tbodydatabackend').html(tbodyHTML);
        }
       });
      
    } 


function Reload()
{
    window.location.reload();
}
