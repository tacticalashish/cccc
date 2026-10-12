
var creditscore = "";
var CheckEximAvilability = "";

var userPoints = 0;

//Show icon for select dropdown
document.addEventListener('DOMContentLoaded', function () {
    const dropdownIcon = document.getElementById('dropdownIcon');
    const countrySelect = document.getElementById('CountryFID');
    dropdownIcon.addEventListener('click', function () {
        if (typeof countrySelect.showPicker === 'function') {
            countrySelect.showPicker();
        }
        else {
            countrySelect.focus();
            const event = new MouseEvent('mousedown');
            countrySelect.dispatchEvent(event);
        }
    });
});



$(document).ready(function () {

    $('#btnReset, .preloader').hide();
    $("#btnINR,#btnYen,#btnWon,#btnEuro,#btnUSD,#btnline,#btnbar,#btnradar,.downloadPDFAndEXCEL").css("display", "none");
    BindProduct();   
    if (UserType == 'Trader') {
        const today = new Date();
        const currentDayNumber = today.getDate();
        $("#fromdate,#todate").datepicker({
            minDate: new Date(new Date().getFullYear() - 1, new Date().getMonth(), new Date().getDate()),
            //maxDate: new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate() - currentDayNumber),
            dateFormat: "dd/MM/yy",
        });
    }
    else {
        $("#fromdate,#todate").datepicker({ dateFormat: "dd/MM/yy", });
    }

    if ((productviewbag != "") && (countryviewbag != "") && (Productnameviewbag !== "")) {
       $(".preloader").css("display", "block");
        $('#RangeID_1, #RangeID_4, #RangeID_6, #RangeID_7, #countrycompare, #productcompare').prop('disabled', false);
        $('#spnForecast').prop('disabled', false).css({ 'opacity': '1', 'cursor': 'pointer' });
        $('#MOM').removeClass('active');
        $('#MonthofMonth').val('');

        BindCountry();
        BindRange();
     
    }

   
});

$('#countrycompare').change(function () {

    GenerateGraph();
});
$('#productcompare').change(function () {

    GenerateGraph();
})
document.querySelectorAll(".calendar-icon").forEach(icon => {
    icon.addEventListener("click", function () {
        this.previousElementSibling.focus();
    });
});
$.formattedDate = function (dateToFormat) {

    var dateObject = new Date(dateToFormat);
    var day = dateObject.getDate();
    var month = dateObject.getMonth() + 1;
    var year = dateObject.getFullYear();
    var hour = dateObject.getHours();
    var minutes = dateObject.getMinutes();
    day = day < 10 ? "0" + day : day;
    month = month < 10 ? "0" + month : month;
    minutes = minutes < 10 ? "0" + minutes : minutes;
    var formattedDate = day + "/" + month + "/" + year + "  " + hour + ":" + minutes;
    return formattedDate;
}; 
$('#divCurrency input').click(function () {
    $('#divCurrency input').removeClass('active');
    $(this).addClass('active');
    const selectedValue = $(this).val();
    $('#spnUnitType').val(selectedValue);
    GenerateGraph();
});
$('#divChartType input').click(function () {
    $('#divChartType input').removeClass('active btn-success');
    $(this).addClass('active btn-success');
    const selectedValue = $(this).val();
    $('#ChartType').val(selectedValue);
    GenerateGraph();
});
$('#fromdate').on('change', function () {
    GenerateGraph();
});
$('#todate').on('change', function () {
    GenerateGraph();
});
$('#FromYear').on('change', function () {
    GenerateGraph();
});
$('#ToYear').on('change', function () {
    $(".preloader").show();
    GenerateGraph();
});
$("#ddlproductFID").change(function () {
    $('#IDdownloadPDFAndEXCEL').css('display', 'none');
    $('#countrycompare').html('');
    $('#countrycompare').selectpicker('refresh');

    $('#productcompare').html('');
    $('#productcompare').selectpicker('refresh');
    $('#MOM').removeClass('active');
    $('#MonthofMonth').val('');
    BindCountry();
});
$("#CountryFID").change(function () {
    var countryFID = $('#CountryFID').val();
    if (countryFID != "") {
        $('#IDdownloadPDFAndEXCEL').css('display', 'none');
        $(".preloader").css("display", "block");

        $('#RangeID_1, #RangeID_4, #RangeID_6, #RangeID_7, #countrycompare, #productcompare').prop('disabled', false);
        $('#spnForecast').prop('disabled', false).css({ 'opacity': '1', 'cursor': 'pointer' });
        $('#MOM').removeClass('active');
        $('#MonthofMonth').val('');
        BindRange();
    }
}) 
$(document).on('click', '#spnForecast', function () {
    Flag = 1;
    $('#ForecastFlag').val(Flag);
   
    onclickRange("Monthly", '2', 'RangeID_2');
}); 
$('#MOM').click(function () {
    var mom = $('#MonthofMonth').val();
    if (mom != '') {
        
        $('#RangeID_1, #RangeID_4, #RangeID_6, #RangeID_7, #countrycompare, #productcompare').prop('disabled', false);
        $('#spnForecast').prop('disabled', false).css({ 'opacity': '1', 'cursor': 'pointer' });
        $('#MOM').removeClass('active');
        $('#MonthofMonth').val('');
        BindRange();
    }
    else {
        if (UserType != 'Trader') {
            var noofdays = 730;
            var date = new Date();
            var datefrom = new Date();
            date.setDate(date.getDate() - noofdays);
            $("#fromdate").datepicker("setDate", date);
        }
        else {
            var noofdays = 365;
            var date = new Date();
            var datefrom = new Date();
            date.setDate(date.getDate() - noofdays);
            $("#fromdate").datepicker("setDate", date);
        }
        $("#RangeName").val('Monthly');
        $("#Range").val('2');
        $('#MonthofMonth').val('MOM');
        $("#RangeID_2").addClass(" bg-success active");
        $("#RangeID_2").prop("disabled", true);
        $('#RangeID_1, #RangeID_4, #RangeID_6, #RangeID_7, #countrycompare, #productcompare').prop('disabled', true);
        $('#spnForecast').prop('disabled', true).css({ 'opacity': '0.5', 'cursor': 'not-allowed' });
        $('#MOM').addClass('active');
        $('#btnReset').show();
        
        $('#divfromdate,#divtodate').show();
        $('#divfromdate,#divtodate').css('display', 'block');
        $('#divFromYear,#divToYear').hide();
        onclickRange('Monthly', '2', 'RangeID_2')
    }
});
function BindDates() {
    
    var numberOfDays = 30;
    var rangeText = $("#RangeName").val();
    if (rangeText == 'Monthly') {
        numberOfDays = 365;
    }

    else if (rangeText == 'Weekly') {
        numberOfDays = 365;
    }

    var date = new Date();
    var datefrom = new Date();
    date.setDate(date.getDate() - numberOfDays);
    var mom = $('#MonthofMonth').val();
    if (mom == '') {
        $("#fromdate").datepicker("setDate", date);
    }

    if (rangeText == 'Monthly') {
        const current = new Date();
        current.setMonth(current.getMonth() - 1);

        const previousMonth = current.toLocaleString('en-US', { month: 'long' });
        let Year = new Date().getFullYear();
        if (previousMonth == "December") {
            Year = Year - 1;
        }

        var dateto = previousMonth + '/' + Year;
        var dateParts = dateto.split('/');
        var monthS = dateParts[0];
        var yearS = parseInt(dateParts[1]);

        var lastDay = new Date(yearS, new Date(Date.parse(monthS + " 1, " + yearS)).getMonth() + 1, 0).getDate();

        dateto = lastDay + '/' + monthS + '/' + yearS;
        $("#todate").val(dateto);

    }
    else {
        $("#todate").datepicker("setDate", datefrom);
    }
}
function BindYear() {
    var currentDate = new Date();
    $('#FromYear').val((currentDate.getFullYear() - 5));
    $('#ToYear').val(currentDate.getFullYear());
}
function BindDistinctYear() {
    var innerHTML = "<option>loading...</option>"
    $('#FromYear,#ToYear').html(innerHTML);

    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/GetDistinctYear",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            innerHTML = "<option value=''>Select Year</option>"

            $(response).each(function (index, value) {
                innerHTML += "<option value='" + value.Year + "'>" + value.Year + "</option>"
            });
            $('#FromYear,#ToYear').html(innerHTML);
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });

}
function BindProductCompare() {

    var countryFID = $('#CountryFID').val();
    var categoryFID = $('#ddlcategoryFID').val();
    var ddlproductFID = $('#ddlproductFID').val();
    var ddlCompare = $("#productcompare");
    var rangeFID = $('#Range').val();

    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/ProductComparison",
        data: "{'countryFID':" + countryFID + ",'rangeFID':" + rangeFID + ",'myParentProductID':" + ddlproductFID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            innerHTML = "";
            $(response).each(function (index, value) {
                if (value.ID != ddlproductFID) {
                    if (value.ID == 0) {
                        innerHTML += "<option class='feedstock color yellow' value='" + value.ID + "'>" + value.ProductName + "</option>"
                    }
                    else {
                        innerHTML += "<option value='" + value.ID + "'>" + value.ProductName + "</option>"
                    }
                }
            });

            $('#productcompare').html(innerHTML);
            $('#productcompare').selectpicker('refresh');
            $('#productcompare').selectpicker('toggle');

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function BindCountryCompare() {

    var regionFID = $('#ddlRegionFID').val();
    var categoryFID = $('#ddlcategoryFID').val();
    var countryFID = $('#CountryFID').val();
    var ddlproductFID = $('#ddlproductFID').val();
    var ddlCompare = $("#countrycompare");
    var rangeFID = $('#Range').val();
    if (ddlproductFID != null && countryFID != null && rangeFID != null) {
        $.ajax({
            type: "POST",
            url: "/AdminCommodityPricing/CountryComparison",
            data: "{'countryFID':" + countryFID + ",'productFID':" + ddlproductFID + ",'rangeFID':" + rangeFID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                innerHTML = "";
                $(response).each(function (index, value) {
                    if (value.ID != countryFID) {

                        innerHTML += "<option value='" + value.CountryName + "'>" + value.CountryName + "</option>"
                    }
                });

                $('#countrycompare').html(innerHTML);
                $('#countrycompare').selectpicker('refresh');
                $('#countrycompare').selectpicker('toggle');

            },
            failure: function (response) {
                console.log(response)
            },
            error: function (response) {
                console.log(response)
            }
        });
    }
    else {
        $('#countrycompare').html('');
        $('#countrycompare').selectpicker('refresh');
    }
}
function GenerateGraph() {
   
    $(".preloader").show();
    var mom = $('#MonthofMonth').val();
    $("#spnTrigger").addClass("showAlert");
    $('#btnINR,#btnYen,#btnWon,#btnEuro,#btnUSD').hide();
    countryUnit = $('#CountryFID option:selected').text();
    var product = $('#ddlproductFID').val();
    var checkcom = $('#countrycompare').selectpicker('val');
    var ForecastFlag = $('#ForecastFlag').val(Flag);
    var selectedValue = $('#spnUnitType').val();
    var Range = $("#Range").val();
    var SelectedRange = $("#RangeName").val();

    
        if (countryUnit === "India" && checkcom.length === 0) {
            $("#btnINR").css("display", "block");
            $("#btnYen").css("display", "block");
            $("#btnWon").css("display", "block");
            $("#btnEuro").css("display", "block");
            $("#btnUSD").css("display", "block");
        }
        else {
            $("#btnINR").css("display", "none");
            $("#btnYen").css("display", "block");
            $("#btnWon").css("display", "block");
            $("#btnEuro").css("display", "block");
            $("#btnUSD").css("display", "block");
        }
    
   
    var ddlCountryCompare = $('#countrycompare').selectpicker('val');
    if (ddlCountryCompare.length != 0) {
        if (selectedValue == "INR") {
            $('#divCurrency input').removeClass('active');
            $('#btnUSD').addClass('active');
            $('#spnUnitType').val('USD');
        }
        else
        {
            $('#spnUnitType').val(selectedValue);
        }
      
    }
    //.......................for product compare click......................
    var ddlproductFID = $('#ddlproductFID').val();
    var ddlCompare = $('#productcompare').selectpicker('val');
    var comparsionProductIDs = '';

    $(ddlCompare).each(function (indexCompare, valCompare) {
      
        comparsionProductIDs += valCompare + ',';
    });
    comparsionProductIDs += ddlproductFID;
    $('#Products').val(comparsionProductIDs);

    //.......................for Country compare click......................
    var CountryFID = $('#CountryFID option:selected').text();

    // var ForecastFlag = $('#ForecastFlag').val(Flag);
    var ddlCountryCompare = $('#countrycompare').selectpicker('val');
    var comparsionCountryIDs = '';

    $(ddlCountryCompare).each(function (indexCompare, valCompare) {
        comparsionCountryIDs += valCompare + ',';
    });
    comparsionCountryIDs += CountryFID;
    $('#Countries').val(comparsionCountryIDs);

    if (window.myLine) {
        window.myLine.destroy();
    }
    var comp= $('#countrycompare').val();
     //-----------------------------------Check product from compare drop down---------------------------------
    var productName = $('#ddlproductFID').find('option:selected').text();
    var selectedOptions = $('#productcompare option:selected');
    if ((productName == "Liquefied Petroleum Gas (LPG)" || productName == "Diesel" || productName == "Crude Oil" || productName == "Natural Gas")) {
        if ((countryUnit == 'India' && productName == "Diesel" || productName == "Liquefied Petroleum Gas (LPG)" ) && (comp.length == 0)) {
            $('#divCurrency input').removeClass('active');
            $('#spnUnitType').val('INR');
            $('#btnINR').addClass('active');
        }
        else {
            $('#divCurrency input').removeClass('active');
            $('#btnUSD').addClass('active');
            $('#spnUnitType').val('USD');
        }
        
    }
    selectedOptions.each(function () {
        var productName = $(this).text();
        if ((productName == "Diesel" || productName == "Crude Oil" || productName == "Liquefied Petroleum Gas (LPG)" || productName == "Natural Gas" || productName == "Natural Gas (Raw Material)" || productName == "Diesel (Raw Material)" || productName == "Crude Oil (Raw Material)" || productName =="Liquefied Petroleum Gas (LPG)(Raw Material)") && (selectedOptions.length != '0')) {
                $('#divCurrency input').removeClass('active');
                $('#btnUSD').addClass('active');
                $('#spnUnitType').val('USD');
        }
    });
     //-----------------------------------Check product from compare drop down end---------------------------------
    $.ajax({
        url: "/AdminCommodityPricing/ChemicalPricingData?" + $('#frmSearchFilter').serialize(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $(".preloader").hide();
            $("#btnline,#btnbar,#btnradar,.downloadPDFAndEXCEL,.pricing_box").css("display", "block");
            var Type = response.User;
            UserType = Type;
            var Points = response.Credit;
            userPoints = Points;
            var productName = $('#ddlproductFID').find('option:selected').text();
            //-----------------------For trader and customer-------------------------------------------
            if (Type == 'Trader') {
                if ((response.Credit != null) && (response.Credit != 'undefine') && (response.Credit != "")) {
                    $("#creditscore").val(Points);
                    $("#divCreditScore").show();
                }
            }
            else if (Type == 'Customer') {
                if ((Points != null) || (Points != 'undefined') || (Points != "") || (Points != "0")) {
                    $("#divCreditScore").show();
                    $("#creditscore").val(Points);
                }
            }
            else {
                $("#divCreditScore").hide();
            }
            //----------------------------------------------------------------------------------------------------------------

            if (response.IsHide == "True" || response.IsHide == null) {
               $("#divCurrency").hide();
            }
            else {
                $("#divCurrency").show();
            }
            //------forcast to date bind--------------------------
            var index = response.labels;
            lastdateindex = index[index.length - 1];
            lastdateindex = lastdateindex.replace(/F$/, "");
            if (Flag == 1) {
                var numberOfDays = 30;

                if (SelectedRange == 'Monthly') {
                    numberOfDays = 365;
                }
                var date = new Date();
                date.setDate(date.getDate() - numberOfDays);
                var datefrom = new Date();
                datefrom.setDate(datefrom.getDate() + 90);
                var dateParts = lastdateindex.split('-');
                var monthS = dateParts[0];
                var yearS = parseInt(dateParts[1]);
                var lastDay = new Date(yearS, new Date(Date.parse(monthS + " 1, " + yearS)).getMonth() + 1, 0).getDate();
                lastdateindex = lastDay + '/' + monthS + '/' + yearS;
                $("#todate").val(lastdateindex);
            }
                Flag = 0;
                $('#ForecastFlag').val(Flag);
          //------------------------------------------------------------------------------
            if (CheckEximAvilability == "Avilabel") {
                $("#getExim").css("display", "block");
            }
            else {
                $("#getExim").css("display", "none");
            }
  
            if (SelectedRange == "Monthly") {
                BindCommentary_parameters();
            }
            else {
                $('#Commentary_parameters').hide();
            }
            BindAboutProduct();
            if (response != null && response.datasets != null && response.datasets.length > 0) {
                var chartTypeName = $('#ChartType').val();
                var ctx = document.getElementById("myChart").getContext('2d');
                if (window.myLine) {
                    window.myLine.destroy();
                }
                //------------------------------Cross line---------------------------------------------------------
                Chart.plugins.register({
                    afterDraw: function (chart) {
                        if (chart.tooltip._active && chart.tooltip._active.length) {
                            const ctx = chart.chart.ctx;
                            const activePoint = chart.tooltip._active[0];
                            const x = activePoint.tooltipPosition().x;
                            const y = activePoint.tooltipPosition().y;
                            const topY = chart.chartArea.top;
                            const bottomY = chart.chartArea.bottom;
                            const leftX = chart.chartArea.left;
                            const rightX = chart.chartArea.right;

                            ctx.save();
                            ctx.beginPath();
                            ctx.setLineDash([5, 5]); // dashed line
                            ctx.strokeStyle = '#aaa';
                            ctx.lineWidth = 1;

                            // Vertical line
                            ctx.moveTo(x, topY);
                            ctx.lineTo(x, bottomY);
                            ctx.stroke();

                            // Horizontal line
                            ctx.beginPath();
                            ctx.moveTo(leftX, y);
                            ctx.lineTo(rightX, y);
                            ctx.stroke();

                            ctx.restore();
                        }
                    }
                });
                //-------------------------------Cross line end----------------------------------------------------


                if ($(window).width() >= 275 && $(window).width() <= 480) {
                    window.myLine = new Chart(ctx, {
                        options: {
                            legend: {
                                display: true,
                                position: 'top',
                                align: 'center',
                                fullWidth: true,
                                labels: {
                                    boxWidth: 12,
                                    fontColor: '#fff',
                                    padding: 10,
                                    fontSize: 8
                                }
                            },
                            tooltips: {
                                titleFontColor: 'white',
                                callbacks: {
                                    label: function (tooltipItem, data) {
                                       
                                        var dataset = data.datasets[tooltipItem.datasetIndex];
                                        var value = dataset.data[tooltipItem.index];
                                        var categoryId = dataset.CategoryID;

                                        if (categoryId === "29") {
                                            return dataset.label + ': ' + value;
                                        }
                                        if (dataset.minData && dataset.maxData &&
                                            dataset.minData[tooltipItem.index] != null &&
                                            dataset.maxData[tooltipItem.index] != null) {
                                            return dataset.label + ': ' + value +
                                                ' [min:' + dataset.minData[tooltipItem.index] +
                                                ' - max:' + dataset.maxData[tooltipItem.index] + "]";
                                        }
                                        return dataset.label + ': ' + value;
                                    }
                                }
                            },
                            responsive: true,
                            title: {
                                display: true,
                                text: response.ChartTitle,
                                fontColor: 'white'
                            },
                            scales: {
                                xAxes: [{
                                    display: true,
                                    ticks: {
                                        fontColor: 'white'

                                    },
                                    gridLines: {
                                        color: "#4d4d4d", // Change horizontal grid line color
                                        lineWidth: 1 // Adjust thickness if needed
                                    },
                                    scaleLabel: {
                                        display: false,
                                        labelString: 'Month'
                                    }
                                }],
                                yAxes: [{
                                    display: true,
                                    gridLines: {
                                        color: "#4d4d4d", // Change vertical grid line color
                                        lineWidth: 1 // Adjust thickness if needed
                                    },
                                    scaleLabel: {
                                        display: false,
                                        labelString: 'Value'
                                    },
                                    ticks: {
                                        min: response.MinValue,
                                        // stepSize: 2000,
                                        fontColor: 'white',
                                    }
                                }]
                            },
                            plugins: {
                                tooltip: {
                                    enabled: true
                                }
                            },
                            pan: {
                                enabled: true,
                                mode: 'x', // or 'xy' to allow both directions
                                speed: 10,
                                threshold: 10
                            },                                                                                                         
                        },
                        data: response,
                        type: chartTypeName,
                    });
                }
                else {

                    window.myLine = new Chart(ctx, {
                        options: {
                            legend: {
                                display: true,
                                position: 'top',
                                align: 'center',
                                fullWidth: true,
                                labels: {
                                    boxWidth: 12,
                                    fontColor: '#fff',
                                    padding: 10,
                                    fontSize: 12
                                }
                            },
                            responsive: true,
                            title: {
                                display: true,
                                text: response.ChartTitle,
                                fontColor: 'white'
                            },
                            tooltips: {
                                titleFontColor: 'white',
                                callbacks: {
                                    label: function (tooltipItem, data) {
                                      
                                        var dataset = data.datasets[tooltipItem.datasetIndex];
                                        var value = dataset.data[tooltipItem.index];
                                        var categoryId = dataset.CategoryID;

                                        if (categoryId === "29") {
                                            return dataset.label + ': ' + value;
                                        }
                                        if (dataset.minData && dataset.maxData &&
                                            dataset.minData[tooltipItem.index] != null &&
                                            dataset.maxData[tooltipItem.index] != null) {
                                            return dataset.label + ': ' + value +
                                                ' [min:' + dataset.minData[tooltipItem.index] +
                                                ' - max:' + dataset.maxData[tooltipItem.index] + "]";
                                        }
                                        return dataset.label + ': ' + value;
                                    }
                                }
                            },
                            scales: {
                                xAxes: [{
                                    display: true,
                                    ticks: {
                                        fontColor: 'white'
                                    },
                                    gridLines: {
                                        color: "#4d4d4d", // Change horizontal grid line color
                                        lineWidth: 1 // Adjust thickness if needed
                                    },
                                    scaleLabel: {
                                        display: false,
                                        labelString: 'Month'
                                    }
                                }],
                                yAxes: [{
                                    display: true,
                                    gridLines: {
                                        color: "#4d4d4d", // Change vertical grid line color
                                        lineWidth: 1 // Adjust thickness if needed
                                    },
                                    scaleLabel: {
                                        display: false,
                                        labelString: 'Value',
                                    },
                                    ticks: {
                                        min: response.MinValue,
                                        fontColor: 'white',
                                        // stepSize: 2000,
                                    }
                                }],
                            },
                            plugins: {
                                tooltip: {
                                    enabled: true
                                }
                            },
                            pan: {
                                enabled: true,
                                mode: 'x', // or 'xy' to allow both directions
                                speed: 10,
                                threshold: 10
                            },
                        },
                        data: response,
                        type: chartTypeName,
                    });
                }
                //Note: prepare result set table with dynamic
                PrepareTable(response);
               // ShowPopUp();
            }
            else {
                $('#tbodyDynamicTableData').html('');
                $('#Commentary_parameters').hide();
                console.log(response);
                console.log("no data here");
            }
            if (ddlCompare.length > 0) {
                $('#dvDefaultCommentary').hide();
                $('#NewsBlock').hide();
                $("#getDisruptionOfproduct").hide();
            }
            else {
                BindCommentary();
                $('#viewSectoral').hide();
                BindProductNews();
                GetMaintenanceAndShutdown();
            }

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function PrepareTable(jsonResponse) {

    var innerTableHTML = '';
    innerTableHTML += "<thead>";
    innerTableHTML += "<tr style='background-color:#3368c6; color:white;'>";

    $(jsonResponse.TableDataHeading).each(function (idx, elm) {
        innerTableHTML += "<th>" + elm + "</th>";
    });

    innerTableHTML += "</tr>";
    innerTableHTML += "</thead>";
    innerTableHTML += "<tbody>";

    $(jsonResponse.TableDataBody).each(function (indexTableDataBody, valueTableDataBody) {
        innerTableHTML += "<tr>";
        $(valueTableDataBody).each(function (idxBody, valueBody) {
            innerTableHTML += "<td>" + valueBody + "</td>";
        });
        innerTableHTML += "</tr>";
    });
    innerTableHTML += "</tbody>";
    $('#tblChemicalResult').html(innerTableHTML);
}
function BindCommentary() {
    $('#dvDefaultCommentary').hide();
    var countryFID = $('#CountryFID').val();
    var productFID = $('#ddlproductFID').val();
    var type = 'chemical pricing';
    var RangeID = $("#Range").val();
    if (countryFID == null || productFID == null || countryFID == '' || productFID == '') {
        console.log('error: country, product and page number have no values');
        return true;
    }
    
    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/GetCommentaryBasedOnProductFIDAndCountryFIDAndType",
        data: "{ 'countryFID':'" + countryFID + "','productFID':'" + productFID + "','type':'" + type + "','rangeID':'" + RangeID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
          
            if (response.CheckEximAvilability == "Avilabel") {
                $("#getExim").css("display", "block");
            }
            else {
                $("#getExim").css("display", "none");
            }
            if ((response.CFRAvilability == "CFRTRUE")) {
                $('#CFR').show();
            }
            else {
                $('#CFR').hide();
            }
            if (response.CheckPriceCatalogAvilability == "Avilabel") {
                $("#getPriceCatalog").css("display", "block");
            }
            else {
                $("#getPriceCatalog").css("display", "none");
            }

            $('#spnCommentaryTitle').text(response.Title);

            $('#articleCommentaryDescription').html(response.Description);

            $('#TitleCommentaryProducers').hide();
            if (response.Producers != null && response.Producers != '') {
                $('#TitleCommentaryProducers').show();
                $('#CommentaryProducers').text(response.Producers);
            }
            if (response.Title != null && response.Title != '') {
                $('#dvDefaultCommentary').show();  
            }
            else {
                $('#dvDefaultCommentary').hide();
            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
    $('#tblDynamicTableData').find('span').css({ "color": "#fff" });
}
function onclickRange(rangeName, RangeID, ButtonID) {
    $("#productcompare").val('');
    $("#countrycompare").val('');
    var mom = $('#MonthofMonth').val();
    if (window.myLine) {
        window.myLine.destroy();
    }
    $('#divChartType input').removeClass('active btn-success');
    $('#btnline').addClass('active btn-success');
    $("#ChartType").val('line');
    $(".range-btn").removeClass(" active");
    $("#" + ButtonID).addClass(" active");
    $("#RangeName").val(rangeName);
    $("#Range").val(RangeID);
    $("#btnINR,#btnYen,#btnWon,#btnEuro,#btnUSD,#btnline,#btnbar,#btnradar").css("display", "none");
    $("#fromdate,#todate").datepicker({ dateFormat: "dd/MM/yy", });
    BindDates();
    BindDistinctYear();
    BindYear();
    BindProductCompare();
    BindCountryCompare();
    var country = $('#CountryFID option:selected').text();
    var selected = $("#RangeName").val();
    if (country === "India") {
        $('#spnUnitType').val('INR');
        $('#divCurrency input').removeClass('active');
        $('#btnINR').addClass('active');
    }
    else {
        $('#spnUnitType').val('USD');
        $('#divCurrency input').removeClass('active');
        $('#btnUSD').addClass('active');

    }

    if (selected == 'Daily Retail' || selected == 'Daily Bulk') {
        $('#divproductcompare,#divcountrycompare').hide();
        $('#divFromYear,#divToYear').hide();
        $('#divfromdate,#divtodate').show();
      
    }
    else if (selected == 'Yearly') {
        $('#divfromdate,#divtodate').hide();
        $('#divFromYear,#divToYear').show();
        $('#divproductcompare,#divcountrycompare').show();
    }
    else if ((selected == 'Weekly' || selected == 'Monthly') && (mom!="MOM") ) {
        $('#divfromdate,#divtodate').show();
        $('#divfromdate,#divtodate').css('display', 'block');
        $('#divFromYear,#divToYear').hide();
        $('#divproductcompare,#divcountrycompare').show();
    }
    GenerateGraph();
}
function BindProduct() {
 
    var innerHTML = "";
    innerHTML = "<option value='' disabled selected>Select Product *</option>";
    $.ajax({
        type: "GET",
        url: "/AdminCommodityPricing/subscribedProducts",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
           
         
            if (response.length != 0) {
                $(response.DataList).each(function (idx, elm) {
                    var selected = (elm.ProductFID == productviewbag) ? " selected" : "";
                    innerHTML += "<option value='" + elm.ProductFID + "'" + selected + ">" + elm.ProductName + "</option>";
                });
                $('#ddlproductFID').html(innerHTML);
                $('#ddlproductFID').val(productviewbag);
                $('#ddlproductFID').selectpicker('refresh');
                $('#ddlproductFID').selectpicker('render');
                $('#ddlproductFID').selectpicker('toggle');
            }
            else {

            }
          
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function BindCountry() {
  
    var innerHTML = "";
    var ProductId = $('#ddlproductFID').val();
    innerHTML = "<option value=''>Select Country *</option>";

    $('#CountryFID').html('');
    $.ajax({
        type: "GET",
        url: "/AdminCommodityPricing/BindCountryBehalf_Of_Product",
        data: { ProductId: ProductId },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response.length != 0) {
                $(response).each(function (idx, elm) {
                    var selected = (elm.CountryName == countryviewbag) ? " selected" : "";
                    innerHTML += "<option value='" + elm.ID + "'" + selected + ">" + elm.CountryName + "</option>";
                });
                $('#CountryFID').show();
                $('#CountryFID').html(innerHTML);
            }
            else {

            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }

    });
}
function BindRange() {
    var innerHTML = "";
    var ProductId = $('#ddlproductFID').val();
    var CountryId = $('#CountryFID').val();
    //innerHTML = "<option value=''>Select Range</option>";

    $('#Range').html('');
    $.ajax({
        type: "GET",
        url: "/AdminCommodityPricing/Range",
        data: { countryFID: CountryId, productFID: ProductId },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var tbodyHTML = '';
            if (response.length != 0) {
                $(response).each(function (idx, elm) {
                    innerHTML += "<input type='button' title='" + elm.Name + "' id='RangeID_" + elm.ID + "' value='" + elm.Name + "' name='" + elm.ID + "' class='range-btn py-0 px-2 btn btn-sm btn-outline-success border border-success text-light rounded-0' onclick=\"onclickRange('" + elm.Name + "','" + elm.ID + "','RangeID_" + elm.ID + "')\" />";
                });
                $('#divRange').html(innerHTML);
                $('#divRange').selectpicker('toggle');
                $("#RangeID_2").addClass(" active");
                onclickRange('Monthly', '2', 'RangeID_2')
            }
            else {

            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function BindCommentary_parameters() {
   
    var selectedRange = $("#RangeName").val();
    $('#Commentary_parameters').hide();
    var countryFID = $('#CountryFID').val();
    var productFID = $('#ddlproductFID').val();
    var type = 'chemical pricing';

    if (countryFID == null || productFID == null || countryFID == '' || productFID == '') {
        console.log('error: country, product and page number have no values');
        return true;
    }
    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/GetCommentryparametersBasedOnProductFIDAndCountryFIDAndType",
        data: "{'countryFID':'" + countryFID + "','productFID':'" + productFID + "','type':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            
            if (response == null || response == "") {
                $('#Commentary_parameters').hide();
            }
            else if (selectedRange == 'Monthly') {
                $('#Commentary_parameters').show();
            }
            else {
                $('#Commentary_parameters').hide();
            }
            if (response.CreatedTime == null || response.CreatedTime == "" || response.CreatedTime == " ") {
                $('#Snapshotmonth').hide();
            }
            else {
                var formattedDate = ($.formattedDate(new Date(parseInt(response.CreatedTime.substr(6)))));
                let parts = formattedDate.split(/[\/\s:]/);
                let date = new Date(parts[2], parts[1] - 1, parts[0]);
                let prevMonth = new Date(date.setMonth(date.getMonth() - 1)).toLocaleString('en-US', { month: 'long' });
                //$('#Snapshotmonth').html(prevMonth);

                //$('#Snapshotmonth').show();
            }

            if (response.PriceTrend == null || response.PriceTrend == "" || response.PriceTrend == " ")
            {
                $('.PriceTrend').hide();
            }
            else {
                $('#PriceTrend').html(response.PriceTrend);
                $('.PriceTrend').show();
            }

            if (response.RawMaterial == null || response.RawMaterial == "" || response.RawMaterial == " ") {
                $('.RawMaterial').hide();
            } else {
                $('#RawMaterial').html(response.RawMaterial);
                $('.RawMaterial').show();
            }

            if (response.ImportingCountries == null || response.ImportingCountries == "" || response.ImportingCountries == " ") {
                $('.ImportingCountries').hide();
            } else {
                $('#ImportingCountries').html(response.ImportingCountries);
                $('.ImportingCountries').show();
            }

            if (response.ExportingCountries == null || response.ExportingCountries == "" || response.ExportingCountries == " ") {
                $('.ExportingCountries').hide();
            } else {
                $('#ExportingCountries').html(response.ExportingCountries);
                $('.ExportingCountries').show();

            }

            if (response.DemandOutlook == null || response.DemandOutlook == "" || response.DemandOutlook == " ") {
                $('.DemandOutlook').hide();
            }
            else {
                $('.DemandOutlook').show();
                if (response.DemandOutlook == 'High') {
                    $('#DemandOutlook').html(response.DemandOutlook + '<i class="bx bx-up-arrow-alt bx-flashing text-primary ml-1 bx-sm"></i>');
                }
                else if (response.DemandOutlook == 'Low') {
                    $('#DemandOutlook').html(response.DemandOutlook + '<i class="bx bx-down-arrow-alt bx-flashing text-danger ml-1 bx-sm"></i>');
                }
                else if (response.DemandOutlook == 'Moderate') {
                    $('#DemandOutlook').html(response.DemandOutlook + '<i class="bx bx-equalizer  bx-flashing text-success ml-1"></i>');
                }
            }

            if (response.SupplyDynamics == null || response.SupplyDynamics == "" || response.SupplyDynamics == " ") {
                $('.SupplyDynamics').hide();
            }
            else {
                $('.SupplyDynamics').show();
                if (response.SupplyDynamics == 'High Inventory') {
                    $('#SupplyDynamics').html(response.SupplyDynamics + '<i class="bx bx-up-arrow-alt bx-flashing text-primary ml-1 bx-sm"></i>');
                }
                else if (response.SupplyDynamics == 'Low Supply') {
                    $('#SupplyDynamics').html(response.SupplyDynamics + '<i class="bx bx-down-arrow-alt bx-flashing text-danger ml-1 bx-sm"></i>');
                }
                else if (response.SupplyDynamics == 'Moderate Supply') {
                    $('#SupplyDynamics').html(response.SupplyDynamics + '<i class="bx bx-equalizer  bx-flashing text-success ml-1"></i>');
                }

            }

            if (response.CurrencyFluctuation == null || response.CurrencyFluctuation == "" || response.CurrencyFluctuation == " ") {
                $('.CurrencyFluctuation').hide();
            } else {
                $('#CurrencyFluctuation').html(response.CurrencyFluctuation);
                $('.CurrencyFluctuation').show();

            }

            if (response.LogisticsPortscenario == null || response.LogisticsPortscenario == "" || response.LogisticsPortscenario == " ") {
                $('.LogisticsPortscenario').hide();
            } else {
                $('#LogisticsPortscenario').html(response.LogisticsPortscenario);
                $('.LogisticsPortscenario').show();

            }
            if (response.MarketSituation == null || response.MarketSituation == "" || response.MarketSituation == " ") {
                $('.MarketSituation').hide();
            }
            else {
                if (response.MarketSituation == "Bullish") {
                    $('#MarketSituation').html(response.MarketSituation + '<i class="bx bx-up-arrow-alt bx-flashing text-primary ml-1 bx-sm"></i>');
                }
                else if (response.MarketSituation == "Bearish") {
                    $('#MarketSituation').html(response.MarketSituation + '<i class="bx bx-down-arrow-alt bx-flashing text-danger ml-1 bx-sm"></i>');
                }
                else if (response.MarketSituation == "Stable") {
                    $('#MarketSituation').html(response.MarketSituation + '<i class="bx bx-equalizer  bx-flashing text-success ml-1"></i>');
                }
                $('.MarketSituation').show();
            }

            if (response.LeadTime == null || response.LeadTime == "" || response.LeadTime == " ") {
                $('.LeadTime').hide();
            } else {
                $('#LeadTime').html(response.LeadTime);
                $('.LeadTime').show();
            }

            if (response.NEWPlantShutdown == null || response.NEWPlantShutdown == "" || response.NEWPlantShutdown == " ") {
                $('.PlantShutdown').hide();
            } else {
                if (response.NEWPlantShutdown.length > 100) {
                    $('#PlantShutdown').html(response.NEWPlantShutdown.substring(0, 100) + '...');

                }
                else {
                    $('#PlantShutdown').html(response.NEWPlantShutdown);


                }

                $('#plantshutdownhidden').val(response.NEWPlantShutdown);
                $('.PlantShutdown').show();

            }
        },
        timeout: 65000,
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function Plantshow() {
    var plant = $('#plantshutdownhidden').val();
    $('#shutdownshow').html(plant);

}
function GetCommentaryBasedOnSearchFilter() {
    var URL = "/AdminCommodityPricing/GetCommentaryBasedOnSearchFilter?Type=chemical Pricing";
    var countryFID = $('#CountryFID').val();
    var productFID = $('#ddlproductFID').val();
    var pageNumber = $('#PageNumber').val();

    if (countryFID == null || productFID == null || pageNumber == null || countryFID == '' || productFID == '' || pageNumber == '') {
        console.log('error: country, product and page number have no values');
        return true;
    }
    URL += '&Country=' + countryFID;
    URL += '&Product=' + productFID;
    URL += '&PageNumber=' + pageNumber;
    $.ajax({
        url: URL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (json) {
            var tbodyInnerHTML = '';
            if (json != null && json != 'f' && json.DateList != null && json.DateList.length > 0) {
                $(json.DateList).each(function (idx, elm) {
                    tbodyInnerHTML += "<tr>";
                    tbodyInnerHTML += "<td>" + elm.RowNumber + "</td>";
                    tbodyInnerHTML += "<td>" + elm.Title + "</td>";
                    //tbodyInnerHTML += "<td style='white-space:pre-line!important;'>" + elm.Description + "</td>";
                    tbodyInnerHTML += "<td>" + elm.Description + "</td>";
                    tbodyInnerHTML += "<td>" + elm.CreatedTimeView + "</td>";
                    tbodyInnerHTML += "</tr>"
                });

                $('#divPagging').html(GetPaginationHTML(json));
                $('#pageSize').val(json.PageSize);
            }
            else {
                tbodyInnerHTML = "<tr><td class='text-center' colspan='20'><span>No records found..!!</span> </td></tr>";
            }
            $('#tbodyDynamicTableData').html(tbodyInnerHTML);
            $('#headingProductName').text($('#ddlproductFID option:selected').text());
            $('#custCommentary').modal('show');
        }
    });
    $('#tblDynamicTableData').find('span').css({ "color": "#fff" });
}
function GetPaginationHTML(json) {
    var pagingInnerHTML = "<span class='pull-left pr-2' style='color:#fff'>(Showing " + json.ShowingFrom + " to " + json.ShowingTo + " of " + json.RecordsCount + " records)</span>";
    var getPaginationButton = GetPagination(json.ActivePage, json.PagerSize, json.NumberOfPages, "");
    pagingInnerHTML += getPaginationButton;
    return pagingInnerHTML;
}
function PaggingButton(elm) {
    var pageNumber = elm.attr('url');
    $('#PageNumber').val(pageNumber);
    GetCommentaryBasedOnSearchFilter();
}
function GetPagination(activePage, pagerSize, numberOfPages, URL) {

    var innerHTML = '';
    var startLi = pagerSize / pagerSize;
    var endLi = pagerSize;
    var multiplier = activePage / pagerSize;
    var modulo = activePage % pagerSize;
    var virtualLi = pagerSize - pagerSize;
    var previous = activePage / pagerSize * pagerSize - pagerSize + 1;
    var next = activePage / pagerSize * pagerSize + pagerSize + 1;

    if (modulo == 0) {
        next = next - pagerSize;
        previous = previous - pagerSize;
    }

    if (activePage > pagerSize) {
        for (var i = 1; i <= multiplier; i++) {
            virtualLi += pagerSize;
        }

        if (modulo > 0) {
            startLi = virtualLi + 1;
        }
        else {
            startLi = virtualLi - pagerSize + 1;
        }
        endLi = startLi + pagerSize - 1;
    }

    innerHTML = "<ul class='pagination pagination-sm no-margin pull-right'>";
    if (activePage > 1 && activePage < pagerSize + 1) {
        innerHTML += "<li class='paginate_button previous disabled' style='display:none;'>";
        innerHTML += "<a href='javascript:void(0);"

        innerHTML += "'><i class='fa fa-fw fa-arrow-left'></i></a>";
        innerHTML += "</li>";

        if (activePage == 1) {
            innerHTML += "<li class='paginate_button previous disabled'>";
            innerHTML += "<a href='javascript:void(0);";
            innerHTML += "'>&laquo;</a>";
            innerHTML += "</li>";
        }
        else {
            innerHTML += "<li class='paginate_button previous'>";
            innerHTML += "<a href='javascript:void(0);'  url='";
            innerHTML += URL + (activePage - 1);
            innerHTML += "' onclick='PaggingButton($(this));'>&laquo;</a>";
            innerHTML += "</li>";
        }
    }
    else if (activePage == 1) {
        innerHTML += "<li class='paginate_button previous disabled' disabled style='display:none;'>";
        innerHTML += "<a href='javascript:void(0);";
        innerHTML += "'><i class='fa fa-fw fa-arrow-left'></i></a>";
        innerHTML += "</li>";
        innerHTML += "<li class='paginate_button previous disabled'>";
        innerHTML += "<a href='javascript:void(0);";
        innerHTML += "'>&laquo;</a>";
        innerHTML += "</li>";
    }
    else {
        innerHTML += "<li class='paginate_button previous'>";
        innerHTML += "<a href='javascript:void(0);'  url='";
        innerHTML += URL + (previous);
        innerHTML += "' onclick='PaggingButton($(this));'><i class='fa fa-fw fa-arrow-left'></i></a>";
        innerHTML += "</li>";
        innerHTML += "<li class='paginate_button previous'>";
        innerHTML += "<a href='javascript:void(0);'  url='";
        innerHTML += URL + (activePage - 1);
        innerHTML += "' onclick='PaggingButton($(this));'>&laquo;</a>";
        innerHTML += "</li>";
    }
    for (var i = startLi; i <= endLi && i <= numberOfPages; i++) {
        if (i == activePage) {
            innerHTML += "<li>";
            innerHTML += "<a style='background-color:gainsboro;' href='javascript:void(0);";
        }
        else {
            innerHTML += "<li class='paginate_button'>";
            innerHTML += "<a onclick='PaggingButton($(this));' href='javascript:void(0);'  url='";
            innerHTML += URL + i;
        }

        innerHTML += "' >";
        innerHTML += i;
        innerHTML += "</a>";
        innerHTML += "</li>";
    }

    if (activePage + pagerSize > numberOfPages) {
        if (activePage == numberOfPages) {
            innerHTML += "<li class='paginate_button next disabled'>";
            innerHTML += "<a href='javascript:void(0);";
            innerHTML += "'>&raquo;</a>";
            innerHTML += "</li>";
        }
        else {
            innerHTML += "<li class='paginate_button next'>";
            innerHTML += "<a href='javascript:void(0);'  url='";
            innerHTML += URL + (activePage + 1);

            innerHTML += "' onclick='PaggingButton($(this));'>&raquo;</a>";
            innerHTML += "</li>";
        }

        innerHTML += "<li class='paginate_button next disabled' style='display:none;'>";
        innerHTML += "<a href='javascript:void(0);";
        innerHTML += "'><i class='fa fa-fw fa-arrow-right'></i></a>";
        innerHTML += "</li>";
    }
    else if (activePage == numberOfPages) {
        innerHTML += "<li class='paginate_button next disabled'>";
        innerHTML += "<a href='javascript:void(0);";
        innerHTML += "'>&raquo;</a>";
        innerHTML += "</li>";
        innerHTML += "<li class='paginate_button next disabled'> style='display:none;'";
        innerHTML += "<a href='javascript:void(0);";
        innerHTML += "'><i class='fa fa-fw fa-arrow-right'></i></a>";
        innerHTML += "</li>";
    }
    else {
        innerHTML += "<li class='paginate_button next'>";
        innerHTML += "<a href='javascript:void(0);'  url='";
        innerHTML += URL + (activePage + 1);

        innerHTML += "' onclick='PaggingButton($(this));'>&raquo;</a>";
        innerHTML += "</li>";

        innerHTML += "<li class='paginate_button next'>";
        innerHTML += "<a href='javascript:void(0);'  url='";
        innerHTML += URL + (next);

        innerHTML += "' onclick='PaggingButton($(this));'><i class='fa fa-fw fa-arrow-right'></i></a>";
        innerHTML += "</li>";
    }
    innerHTML += "</ul>";

    return innerHTML;
}
function Reset() {
    $('#PageNumber').val('1');
}
function BindProductNews() {
    
    var Productname = $("#ddlproductFID option:selected").text();
    $.ajax({

        type: "POST",
        url: "/AdminCommodityPricing/BindProductNews",
        data: "{ 'ProductName':'" + Productname + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            
            innerHTML = "";

            $('#news-container').html('');
            var newsContainer = $('#news-container');

            if (response.length == 0) {

            }
            else {
                $.each(response, function (index, item) {

                    var data = item.NewsDiscription;
                    var limiteddata = truncateString(data, 20)//data.Substring(0,200);
                    var date = item.CreatedTime;
                    if (date != null) {
                        var formattedDate = ($.formattedDate(new Date(parseInt(date.substr(6)))));
                    }

                    var url = generateItemNameAsParam(item.URL, item.ID)

                    var newsItem = $('<div class="col-md-6">' +
                        '<div class="news-cont">' +
                        '<aside class="image-cont">' +
                        (item.NewsImg != "" && item.NewsImg != null ?
                            '<img class="image-news" src="https://www.chemanalyst.com/AdminProductImage/' + item.NewsImg + '" alt="" />' :
                            '<img class="image-news" src="~/images/3.jpg" alt="" />') +
                        '</aside>' +
                        '<div class="news-test">' +
                        '<h6 class="text-dark"><a target="_blank" href="/NewsAndDeals/NewsDetailsUI/' + item.ID + '">' + item.NewsName + '</a></h6>' +
                        limiteddata + '...' + '<p class="formate_date">' + formattedDate + '</p>' +
                        '</div>' +
                        '</div>' +
                        '</div>'
                    );

                    // Append the created news item to the container
                    newsContainer.append(newsItem);
                    $('#NewsBlock').show();
                });
            }

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function truncateString(input, words) {
    return input.split(' ').slice(0, words).join(' ');
}
function generateItemNameAsParam(URL, id) {

    var phrase = URL + "-" + id; // Creates in the specific patterns
    var str = getByteArray(phrase).toLowerCase();
    str = str.replace(/[^a-z0-9\s-]/g, ""); // Remove invalid characters for param
    str = str.replace(/\s+/g, "-").trim(); // Convert multiple spaces into one hyphen
    str = str.replace(/\s/g, "-"); // Replaces spaces with hyphens
    return str;
}
function getByteArray(input) {
    return input;
}
function GetAllNews() {

    var Productname = $("#ddlproductFID option:selected").text();
    var url = "/NewsAndDeals/GetNewsBasedOnProductFilter?Productname=" + Productname + "";
    window.open(url, '_blank');

}
function OpenExim() {
    var ddlproductFID = $('#ddlproductFID').val();
    ddlproductFID = '';
    window.open('/AdminChemicalPricing/AdminExim?id=' + ddlproductFID, '_blank', 'noopener,noreferrer');
}

function OpenPriceCatalog() {
    var ddlproductFID = $('#ddlproductFID').val();
    var CountryId = $('#CountryFID').val();

    $.ajax({
        type: "POST",
        url: '/AdminCommodityPricing/SetPriceCatalogParams',
        data: { productId: ddlproductFID, countryId: CountryId },
        success: function () {

            window.open('/ChemicalPricing/CompanyPrices', '_blank');
        },
        error: function (xhr, status, error) {
            console.error("Failed to set parameters:", error);
        }
    });
}

function SetTriggers() {

    window.open('/ChemicalPricing/SetTrigger', '_blank', 'noopener, noreferrer');
} 
function BindAboutProduct() {
    var productFID = $('#ddlproductFID').val();
    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/BindAboutProduct",
        data: "{ 'productFID':'" + productFID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            var tbodyHTML = '';
            var data = response;
            if (response.length != 0) {
                $(data).each(function (idx, elm) {
                    tbodyHTML += "<tr>";
                    tbodyHTML += "<td>" + elm.AboutProduct + "</td>";
                    tbodyHTML += "</tr>";

                });

                $('#aboutProductDesc').html(tbodyHTML);
                $('#aboutProduct').show();
            }
            else {
                $('#aboutProductDesc').html('');
                $('#aboutProduct').hide();
            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function DownloadPDf() {
    
    var productcompare = $("#productcompare").selectpicker('val');

    if ($("#creditscore").val() == "") {
        PdfExport();
    }
    else if (($("#creditscore").val() > 0) && ((productcompare == '') || (productcompare == null))) {
        Maintainlog_CreditScore('PDF');
    }
    else if (($("#creditscore").val() == 0) && ((productcompare == '') || (productcompare == null))) {
        Maintainlog_CreditScore('PDF');
    }
    else if (((productcompare != '') || (productcompare != null))) {

        Maintainlog_CreditScore('PDF');

            //swal({
            //    title: "Download is not allowed on ProductCompare.",
            //    type: "Success"
            //}).then(function () {
            //});
    }
    else {

        swal({
            title: "Download limit exceeded",
            type: "Success"
        }).then(function () {
        });
    }
} 
function Maintainlog_CreditScore(DownloadType) {
    var freeTrail = {};
    freeTrail.ProductID = $('#ddlproductFID').val();
    freeTrail.Credit = $('#creditscore').val();
    freeTrail.DownloadType = DownloadType;
    freeTrail.Productcompare = $("#productcompare").selectpicker('val').join(',');


    $.ajax({
        type: "GET",
        url: "/AdminCommodityPricing/GetMaintainlog_CreditScore",
        data: freeTrail,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $(response).each(function (idx, elm) {
                
                var Message = elm.ValidationMessage;
                if (Message == "Exists") {
                    if (DownloadType == "PDF") {
                        PdfExport();
                    }
                    else if (DownloadType == "Excel") {
                        ExcelExport();
                    }
                }
                if (Message == "NOT Exists") {
                    swal({
                        title: "Download limit exceeded",
                        type: "Success"
                    }).then(function () {
                    });
                }

                if (Message == "Product Compare NOT Exists") {
                    swal({
                        title: "Please export only downloaded products",
                        type: "Success"
                    }).then(function () {
                    });
                }
                if (Message == "Updated") {
                    if (DownloadType == "PDF") {
                        PdfExport();
                    }
                    else if (DownloadType == "Excel") {
                        ExcelExport();
                    }
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
function PdfExport() {
    
    $('#myChart').css({ "background-color": "#30353b" });
    $('#invoice').css({ "background-color": "#30353b" });
    $('#myChart').css({ "color": "#fff" });
    $('#myChart').css({ "margin": "0 auto" });
    

    $('.virtual-class-pdf').addClass('pdf-style');
    $('#spnUnitType').css({ 'color': '#fff', 'font-size': '14px', 'margin-top': '2px', 'border': 'none' });

    Chart.defaults.global.defaultFontColor = '#fff';
    var chartButtonID = $('.chart-type-active').attr('id');
    $('#' + chartButtonID).trigger('click');
    setTimeout(
        function () {
            PdfExportDeley()
        }, 1000);
    setTimeout(
        function () {
            $('#myChart').css({ "background-color": "#30353b" });
            $('#invoice').css({ "background-color": "#30353b" });
            $('#spnUnitType').css({ 'color': '#fff', 'margin-top': '0px', 'font-size': '10px', 'margin-top': '0px', 'border': '1px solid #dcdcdc' });
            Chart.defaults.global.defaultFontColor = '#fff';
            $('#' + chartButtonID).trigger('click');
            $('.virtual-class-pdf').removeClass('pdf-style');
            $('#fullZoomBtn').show();
            $('#google_translate_element').show();
            $('#viewAll').show();           
        }, 1100);


}
function PdfExportDeley() {
    var currentYear = new Date().getFullYear();
    var element = document.getElementById('invoice');
    var productName = $('#ddlproductFID').find('option:selected').text();
    var rangeName = $('#RangeName').val();
    var fileName = productName + "_" + rangeName + "_" + "Commodity Pricing" + ".PDF";
    var mode = "avoid-all";
    var pagebreak = (mode === 'specify') ?
        { mode: '', before: '.before', after: '.after', avoid: '.avoid' } :
        { mode: mode };

    
    const snapshot = document.getElementById('monthly_snapshot');
  
    const svgs = snapshot.querySelectorAll('svg');
    svgs.forEach(svg => {
        svg.setAttribute('width', '22');
        svg.setAttribute('height', '22');
        svg.style.width = '22px';
        svg.style.height = '22px';

        if (!svg.getAttribute('viewBox')) {
            const bbox = svg.getBBox();
            svg.setAttribute('viewBox', `0 0 ${bbox.width} ${bbox.height}`);
        }

      
    });

    $("#myChart").css("width", "100%");
    $(".pdfheader").show();
    $(".pdffooter").show();
    $('#fullZoomBtn').hide();
    $('#viewAll').hide();
    $('#google_translate_element').hide();   
    $("#pfooter").html("&copy;  Copyright " + currentYear + " | ChemAnalyst | All Rights Reserved | " + Date($.now()) + " | " + UserName + "");

    let settings = {
        filename: fileName,
        margin: 1,
        pagebreak: pagebreak,
        html2canvas: { scale: 2 },
        jsPDF: { orientation: 'l', unit: 'in', format: 'A3', compressPDF: true }
    };

    if ($(window).width() <= 760) {
        settings.jsPDF.orientation = 'p';
        settings.html2canvas.scale = 1;
    }

    html2pdf().from(element).set(settings).save().then(function () {
        $(".pdfheader").hide();
        $(".pdffooter").hide();
    });
}
function DownloadExcel() {
    var productcompare = $("#productcompare").selectpicker('val');
    if ($("#creditscore").val() == "") {

        ExcelExport();
    }
    else if (($("#creditscore").val() > 0) && ((productcompare == '') || (productcompare == null))) {

        Maintainlog_CreditScore('Excel');

    }
    else if (($("#creditscore").val() == 0) && ((productcompare == '') || (productcompare == null))) {

        Maintainlog_CreditScore('Excel');

    }
    else if (((productcompare != '') || (productcompare != null))) {

            Maintainlog_CreditScore('Excel');
            //swal({
            //    title: "Download is not allowed on ProductCompare.",
            //    type: "Success"
            //}).then(function () {
            //});
    }
    else {

        swal({
            title: "Download limit exceeded",
            type: "Success"
        }).then(function () {
        });
    }
}
function ExcelExport() {

    var productName = $('#ddlproductFID').find('option:selected').text();
    var rangeName = $('#RangeName').val();
    var countryName = $('#CountryFID').find('option:selected').text();
    var fileName = productName + "_" + rangeName + "_" + "Commodity Pricing" + ".xls";
    var sheetname = productName;

    var currency = '';
    if (countryName == "India") {
        if ((productName == "Natural Gas") && ($('#countrycompare').selectpicker('val') == "")) {
            currency = "[Unit = (USD/1000MMBtu)]";
        }
        else if ((productName == "Natural Gas") && ($('#countrycompare').selectpicker('val') != "")) {
            currency = "[Unit = Natural Gas Unit Price (EUR/1000MWh) in Europe,Other in (USD/1000MMBtu)]";
        }
        else if ((productName == "Crude Oil") && ($('#productcompare').selectpicker('val') == "")) {
            currency = "[Unit = USD/Barrel]";
        }
        else if ((productName == "Crude Oil") && ($('#productcompare').selectpicker('val') != "")) {
            currency = "[Unit = Crude Oil USD/Barrel and Other Products USD/Tonne]";
        }
        else {
            currency = $("#spnUnitType").val();
        }
    }
    else {
        if ((productName == "Natural Gas") && ($('#countrycompare').selectpicker('val') == "")) {
            currency = "[Unit = (USD/1000MMBtu)]";
        }
        else if ((productName == "Natural Gas") && ($('#countrycompare').selectpicker('val') != "")) {
            currency = "[Unit = Natural Gas Unit Price (EUR/1000MWh) in Europe,Other in (USD/1000MMBtu)]";
        }
        else if ((productName == "Crude Oil") && ($('#productcompare').selectpicker('val') == "")) {
            currency = "[Unit = USD/Barrel]";
        }
        else if ((productName == "Crude Oil") && ($('#productcompare').selectpicker('val') != "")) {
            currency = "[Unit = Crude Oil USD/Barrel and Other Products USD/Tonne]";
        }
        else {
            currency = $("#spnUnitType").val();
        }
    }

    if (($('#countrycompare').selectpicker('val') != "") && (productName != "Natural Gas")) {
        currency = "[Unit = USD/Tonne]";
        countryName = '';
    }

    var table = document.getElementById("tblChemicalResult");

    // Get the last row of the table
    var lastRow = table.rows[table.rows.length - 1];

    // Get the number of columns in the last row
    var numColumns = lastRow.cells.length;

    // Create a new row after the last row
    var newRow = table.insertRow(table.rows.length);

    // Create cell for copyright notice
    var cellCopyright = newRow.insertCell(0);
    cellCopyright.colSpan = numColumns; // Span all columns
   // cellCopyright.textContent = currency + " || " + countryName + " || Note: The current and future months' values are forecasted in Monthly Price || Copyright 2024-Chem Analyst. All Rights Reserved";
    cellCopyright.textContent = countryName + " || Note: The current and future months' values are forecasted in Monthly Price || Copyright 2025-Chem Analyst. All Rights Reserved";
    $("#tblChemicalResult").table2excel({

        exclude: ".noExl",
        name: "Worksheet Name",
        filename: fileName,
        fileext: ".xls",
        sheetName: sheetname

    });
    table.deleteRow(table.rows.length - 1);
}
// chart graph zooming code
document.getElementById("fullZoomBtn").addEventListener("click", function () {
    let chartContainer = document.getElementById("chartContainer");

    if (!document.fullscreenElement) {
        if (chartContainer.requestFullscreen) {
            chartContainer.requestFullscreen();
        }
        else if (chartContainer.mozRequestFullScreen) { // Firefox
            chartContainer.mozRequestFullScreen();
        }
        else if (chartContainer.webkitRequestFullscreen) { // Chrome, Safari & Opera
            chartContainer.webkitRequestFullscreen();
        }
        else if (chartContainer.msRequestFullscreen) { // IE/Edge
            chartContainer.msRequestFullscreen();
        }
        // Adjust canvas size when entering fullscreen
        canvas.style.width = "100vw";
        canvas.style.height = "100%";
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
        // Adjust canvas size when entering fullscreen
        canvas.style.width = "100vw";
        canvas.style.height = "100%";
    }
});
function ShowPopUp() {
    //.......................for product compare click......................
    var Currency = $('#spnUnitType').val();
    var ddlproductFID = $('#ddlproductFID').val();
    var ddlCompare = $('#productcompare').selectpicker('val');
    var comparsionProductIDs = '';

    $(ddlCompare).each(function (indexCompare, valCompare) {

        comparsionProductIDs += valCompare + ',';
    });
    comparsionProductIDs += ddlproductFID;
    $('#Products').val(comparsionProductIDs);

    //.......................for Country compare click......................
    var CountryFID = $('#CountryFID option:selected').text();

    // var ForecastFlag = $('#ForecastFlag').val(Flag);
    var ddlCountryCompare = $('#countrycompare').selectpicker('val');
    var comparsionCountryIDs = '';

    $(ddlCountryCompare).each(function (indexCompare, valCompare) {
        comparsionCountryIDs += valCompare + ',';
    });
    comparsionCountryIDs += CountryFID;
    $('#Countries').val(comparsionCountryIDs);

    $.ajax({
        type: "GET",
        url: "/AdminCommodityPricing/ShowPopUp",
        data: { CountryesId: comparsionCountryIDs, ProductsId: comparsionProductIDs, Currency: Currency},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response.length != 0) {
                $(response).each(function (idx, elm) {
                    if (elm.ValidationMessage == "ShowPopup") {
                        $('#PopupForUnit').modal('show');
                    }
                    else {
                        $('#PopupForUnit').modal('hide');
                    }

                });

            
            }
           
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        } 
    });
}


function GetCFRPricing() {
    var productFID = $('#ddlproductFID').val();
    var countryFID = $('#CountryFID').val();

    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/BindCFRPricingBasedonProductandcountry",
        data: "{ 'productFID':" + productFID + ",'countryFID':" + countryFID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var tbodyHTML = '';
            $('#tbodyCFRTableData').html('');
            var data = response;
            if (response.length != 0) {
                $(data).each(function (idx, elm) {
                    tbodyHTML += "<tr>";
                    tbodyHTML += "<td>" + elm.CountryName + "</td>";
                    tbodyHTML += "<td>" + elm.Year + "</td>";
                    tbodyHTML += "<td>" + elm.Month + "</td>";
                    tbodyHTML += "<td>" + elm.Counts + "</td>";
                    tbodyHTML += "<td>" + elm.Currency + "</td>";
                    tbodyHTML += "<td>" + elm.Grade + "</td>";
                    tbodyHTML += "</tr>";

                });

                $('#tbodyCFRTableData').html(tbodyHTML);

            }
            else {

                tbodyHTML = "<tr><td class='text-center' colspan='20'><span>No records found..!</span> </td></tr>";
                $('#tbodyCFRTableData').html(tbodyHTML);

            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}


function GetCreditDownloadProducts() {
    $.ajax({
        type: "POST",
        url: "/AdminCommodityPricing/GetCreditDownloadProducts",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var tbodyHTML = '';
            var data = response;
            if (response.length != 0) {
                $(data).each(function (idx, elm) {
                    tbodyHTML += "<tr>";
                    tbodyHTML += "<td>" + elm.ProductName + "</td>";
                    tbodyHTML += "</tr>";
                });
                $('#tbodyCreditData').html(tbodyHTML);
            }
            else {
                tbodyHTML = "<tr><td class='text-center' colspan='20'><span>No records found..!</span> </td></tr>";
                $('#tbodyCreditData').html(tbodyHTML);
            }
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}

function GetMaintenanceAndShutdown() {
    debugger
    var Product = $('#ddlproductFID option:selected').val();
    $.ajax({
        url: "/AdminChemicalPricing/GetMaintenanceAndShutdown ",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { Product: Product },
        success: function (json) {
          debugger
            var tbodyHTML = '';
            if (json.length != 0) {
                $(json).each(function (idx, elm) {
                    var date = elm.CreatedDate
                    var formattedDate = ($.formattedDate(new Date(parseInt(date.substr(6)))));
                    tbodyHTML += "<tr>";
                    tbodyHTML += "<td hidden>" + elm.ID + "</td>";
                    tbodyHTML += "<td hidden>" + elm.Product + "</td>";
                    tbodyHTML += "<td>" + elm.Month + "</td>";
                    tbodyHTML += "<td>" + elm.Year + "</td>";
                    tbodyHTML += "<td>" + elm.Country + "</td>";
                    tbodyHTML += "<td>" + elm.Company + "</td>";
                    tbodyHTML += "<td>" + elm.Location + "</td>";
                    tbodyHTML += "<td>" + elm.Duration + "</td>";
                    tbodyHTML += "<td>" + elm.ActiveCapacityTPM + "</td>";
                    tbodyHTML += "<td>" + elm.TotalCapacityTPM + "</td>";
                    tbodyHTML += "<td>" + elm.TypeOfShutdown + "</td>";
                    tbodyHTML += "<td>" + elm.Insights + "</td>";
                    tbodyHTML += "<td>" + elm.ImpactOnPricing + "</td>";
                    tbodyHTML += "<td>" + elm.CreatedBy + "</td>";
                    tbodyHTML += "<td>" + elm.OutageStartDate + "</td>";
                    tbodyHTML += "<td>" + elm.OutageEndDate + "</td>";
                    //tbodyHTML += "<td>" + elm.OperatingEfficiency + "</td>";
                    tbodyHTML += "</tr>"
                });

                $('.tbodydataMaintenanceAndShutdown').html(tbodyHTML);
                $('#MaintenanceAndShutdown').show();
                $('#NoRecord').hide();
            }
            else {
                $('#MaintenanceAndShutdown').hide();
                $('#NoRecord').show();
            }
        }
    });
}






