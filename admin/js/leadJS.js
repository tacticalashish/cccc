$(document).ready(function () {

    $(function () {
        $('#DateFrom').datetimepicker();
        $('#DateTo').datetimepicker();
    });
});
var GetModuleType = "";
function UploadFile() {

    var files = $("#FileUpload").get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("FileUpload", files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/UploadFiles",
        dataType: "json",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (result, status, xhr) { // alert(result);
        },
        error: function (xhr, status, error) { // alert(status);
        }
    });
}
$("#Country").on("change", function () {
    //$('#Product').selectpicker('val', '');
    //$('#Module').selectpicker('val', '');
    //$('#ChemicalRange').selectpicker('val', '');
    //$('#MarketAnalysisRange').selectpicker('val', '');
    $("#Product").val('');
    $("#ChemicalRange").val('');

    $("#MarketAnalysisRange").val('');
    GetProduct();
});
$("#Region").on("change", function () {
    //$('#Product').selectpicker('val', '');
    //$('#Module').selectpicker('val', '');
    //$('#ChemicalRange').selectpicker('val', '');
    //$('#MarketAnalysisRange').selectpicker('val', '');
    $("#Product").val('');
    $("#ChemicalRange").val('');
    $("#MarketAnalysisRange").val('');
    GetProduct();
});
$("#Product").on('changed.bs.select', function (e, clickedIndex) {
    //$('#Module').selectpicker('val', '');
    //$('#ChemicalRange').selectpicker('val', '');
    //$('#MarketAnalysisRange').selectpicker('val', '');
    $('#ChemicalRange').selectpicker('val', '');
    $('#MarketAnalysisRange').selectpicker('val', '');
    if (GetModuleType == "Chemical") {
        GetchemicalRange();
    }
    else {
        GetMarketAnalysisRange();
    }
});
$.formattedDate = function (dateToFormat) {
    var dateObject = new Date(dateToFormat);
    var day = dateObject.getDate();
    var month = dateObject.getMonth() + 1;
    var year = dateObject.getFullYear();
    day = day < 10 ? "0" + day : day;
    month = month < 10 ? "0" + month : month;
    var formattedDate = day + "/" + month + "/" + year;
    return formattedDate;
};

$(document).ready(function () {
    // $("#todateCompare").datepicker({ dateFormat: "dd/MM/yy" });
    // $("#fromdateCompare").datepicker({ dateFormat: "dd/MM/yy" });
    GetClintEmail();
    GetAllDropdowndata();
  //  GetProduct();
    //GetCompany();
    GetCountry();
    GetDataAnalyst();
});


//$("#ClientName").change(function () {
//    GetAllDropdowndata();
//    GetProduct();
//    GetCompany();
//    GetCountry();
//    GetDataAnalyst();
//});
$("#ClientPrimaryEmail").change(function () {
    
    var ClientPrimaryEmailId = $("#ClientPrimaryEmail").val();
    $.ajax({
        url: "/LeadManagementSubscription/GetClientPrimaryEmailIdRecord",
        type: "POST",
        dataType: "json",
        async: true,
        data: "{ClientPrimaryEmailId: '" + ClientPrimaryEmailId + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(response.DataList).each(function (jsonIndex, Value) {
          
                $("#ClientName").val(Value.Name);
                $("#ClientCompanyName").val(Value.CompanyName);
                $("#ClientPhoneNumber").val(Value.Phone);
                $('#CRM').html("<option value = '" + Value.CrmId + "'> " + Value.CRM + "</option>");
                if (Value.Status == 'Won') {
                    $(".popup-open05").css("display", "none");
                } else {

                    $(".popup-open05").css("display", "block");
                }

            });
          
        },
        failure: function (response) { },
        error: function (response) { }
    });
    GetsecondaryEmail();
});

$("#CancelPOP").click(function () {
    window.location.reload();
    $(".popup-open04").css("display", "none");
});

function GetAllDropdowndata() {
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetAllDropdowndata",
        async: true,
        success: function (data) {
            var innerHTML = "";
            var RegioninnerHTML = "";
            var ModuleinnerHTML = "";
            var Objdic_CRM = "";
            Objdic_CRM += "<option  value = ''>" + "Select CRM" + "</option>";
  RegioninnerHTML += "<option  value = ''>" + "Select Region" + "</option>";
            $(data.DataList.Objdic_Region).each(function (index, value) {
                if (value.RegionName != null || value.RegionName != "" || value.RegionName != undefined) {
                    RegioninnerHTML += "<option class='option01' value = '" + value.RegionId + "'> " + value.RegionName + "</option>"
                }
            });

            $(data.DataList.Objdic_Module).each(function (index, value) {
                if (value.ModuleName != null || value.ModuleName != "" || value.ModuleName != undefined) {
                    ModuleinnerHTML += "<option value = '" + value.ModuleId + "'> " + value.ModuleName + "</option>"
                }
            });
            //$(data.DataList.Objdic_CRM).each(function (index, value) {
            //    if (value.CRM != null || value.CRM != "" || value.CRM != undefined) {
            //        Objdic_CRM += "<option value = '" + value.CrmId + "'> " + value.CRM + "</option>"
            //    }
            //});

            $('#Region').html(RegioninnerHTML);
            $('#Module').html(ModuleinnerHTML);
            $('#Module').selectpicker('refresh');
          //  $('#CRM').html(Objdic_CRM);
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

    var SubscriptionRegionCountryName = "";
    var SubscriptionRegionCountryType = $("#SubscriptionRegionCountry option:selected").text();

    if (SubscriptionRegionCountryType == "Region Wise") {
        SubscriptionRegionCountryName = $("#Region option:selected").text();
    }
    else {
        SubscriptionRegionCountryName = $("#Country option:selected").text();
    }

    SubscriptionRegionCountryName = $.trim(SubscriptionRegionCountryName);

    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetProductByCountryName",
        data: "{SubscriptionRegionCountryName: '" + SubscriptionRegionCountryName + "',Type: '" + SubscriptionRegionCountryType + "',Module: '" + GetModuleType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var ProductinnerHTML = "";

            $(data.DataList).each(function (index, value) {
                if (value.ProductName != null || value.ProductName != "" || value.ProductName != undefined) {
                    ProductinnerHTML += "<option class='option01' value = '" + value.ProductId + "'> " + value.ProductName + "</option>"
                }
            });
            $('#Product').html(ProductinnerHTML);
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
function GetCompany() {
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetClintCompany",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {

            var innerHTML = "";
            innerHTML += "<option  value = ''>" + "Select Company" + "</option>";
            $(data.DataList).each(function (index, value) {
                if (value.CompanyName != null || value.CompanyName != "" || value.CompanyName != undefined) {
                    innerHTML += "<option value = '" + value.CompanyId + "'> " + value.CompanyName + "</option>"
                }
            });
            //$('#ClientCompanyName').html(innerHTML);
            $('#ClientCompany').html(innerHTML);
            
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
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

            $('#Country').html(innerHTML);
            $('#ClientCountryName').html(innerHTML);

        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function GetDataAnalyst() {
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetDataAnalyst",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {

            var innerHTML = "";
            innerHTML += "<option  value = ''>" + "Select Assign Analyst" + "</option>";
            $(data.DataList).each(function (index, value) {
                if (value.AssignAnalystId != null || value.AssignAnalystId != "" || value.AssignAnalystId != undefined) {
                    innerHTML += "<option class='option01' value = '" + value.AssignAnalystId + "'> " + value.AssignAnalyst + "</option>"
                }
            });
            $('#AssignAnalyst').html(innerHTML);
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}

$("#SubscriptionRegionCountry").change(function () {
    $('#Country').selectpicker('val', '');
    $('#Product').selectpicker('val', '');
    $('#Module').selectpicker('val', '');
    $('#ChemicalRange').selectpicker('val', '');
    $('#MarketAnalysisRange').selectpicker('val', '');
 $("#Region").val('');
    var SubscriptionRegionCountry = $("#SubscriptionRegionCountry").val();
    if (SubscriptionRegionCountry == "region") {
        $("#CountryDiv").hide();
        $("#RegionDiv").show();
    } else {

        $("#CountryDiv").show();
        $("#RegionDiv").hide();
    }
});



$('#Module').change('changed.bs.select', function (e, clickedIndex) {
    var selectedValues = $(e.currentTarget).val();
    GetModuleType = "";
    if (selectedValues != null) {
        //-----------------For disruption Tracker (capacity tracker)------------------------------------
        if (selectedValues.indexOf('8') != -1) {
            GetModuleType = "Disruption Tracker";
            GetProduct();
        }
        //-----------------For Chemical Price------------------------------------
        if (selectedValues.indexOf('1') != -1) {
            $('#dvChemicalPricingRange').show();
            GetModuleType = "Chemical";
            // GetchemicalRange();
        }
        else {
            $('#dvChemicalPricingRange').hide();
            $('#ChemicalRange').selectpicker('val', '');
        }

        //-------------------stock Analysis----------------------------
        if ((selectedValues.indexOf('9') != -1) && (selectedValues.indexOf('1') == -1) && (selectedValues.indexOf('2') == -1) && (selectedValues.indexOf('3') == -1) && (selectedValues.indexOf('4') == -1) && (selectedValues.indexOf('5') == -1) && (selectedValues.indexOf('6') == -1) && (selectedValues.indexOf('7') == -1) && (selectedValues.indexOf('8') == -1)) {

            $('#RegionDiv').hide();
            $('#CountryDiv').hide();
            $('#ProductDiv').hide();

        } else {

            var SubscriptionRegionCountry = $("#SubscriptionRegionCountry").val();
            if (SubscriptionRegionCountry == "region") {
                $("#CountryDiv").hide();
                $("#RegionDiv").show();
            } else {

                $("#CountryDiv").show();
                $("#RegionDiv").hide();
            }
            $('#ProductDiv').show();

        }
        //-----------------For Market Analysis ------------------------------------
        if (selectedValues.indexOf('2') != -1) {

            $(this).find('option:first').prop('selected', false);
            //$('#Module #1').css('display', 'none');
            $('#dvMarketAnalysisRange').show();
            GetModuleType = "Market";
            GetMarketAnalysisRange();
        }
        else {
            $('#dvMarketAnalysisRange').hide();
            $('#MarketAnalysisRange').selectpicker('val', '');
        }
    }
    else {
        $('#dvChemicalPricingRange').hide();
        $('#dvMarketAnalysisRange').hide();
        var SubscriptionRegionCountry = $("#SubscriptionRegionCountry").val();
        if (SubscriptionRegionCountry == "region") {
            $("#CountryDiv").hide();
            $("#RegionDiv").show();
        } else {

            $("#CountryDiv").show();
            $("#RegionDiv").hide();
        }
        $('#ProductDiv').show();
    }
    GetCountryByModule();
});
//$('#Module').on('changed.bs.select', function (e, clickedIndex) {
//    var selectedValues = $(e.currentTarget).val();

//    if (selectedValues != null) {
//        if (selectedValues.indexOf('1') != -1) {
//            $('#dvChemicalPricingRange').show();
//            GetchemicalRange();
//        } else {
//            $('#dvChemicalPricingRange').hide();
//            $('#ChemicalRange').selectpicker('val', '');
//        }

//        if (selectedValues.indexOf('2') != -1) {
//            $('#dvMarketAnalysisRange').show();
//            GetMarketAnalysisRange();
//        } else {
//            $('#dvMarketAnalysisRange').hide();
//            $('#MarketAnalysisRange').selectpicker('val', '');
//        }
//    } else {
//        $('#dvChemicalPricingRange').hide();
//        $('#dvMarketAnalysisRange').hide();
//    }

//});
// $("#Product").on('changed.bs.select', function (e, clickedIndex) {
//    GetMarketAnalysisRange();
// });

function GetCountryByModule() {
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetCountryByModuleWise",
        data: "{Module: '" + GetModuleType + "'}",
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

            $('#Country').html(innerHTML);
            $('#Country').selectpicker('refresh');
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}

function GetchemicalRange() {
    
    var ProductList = "";
    ProductList = $('#Product').val();
    var CountryName = $("#Country option:selected").text();
    CountryName = $.trim(CountryName);
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetChemicalRange",
        data: "{ productlist: '" + ProductList + "', CountryName: '" + CountryName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var innerHTML = '';
            $(response).each(function (jsonIndex, Value) {
                innerHTML += "<option product-value='" + Value.ProductId + "'range-value='" + Value.Chemical_RangeID + "' >  " + Value.Chemical_Range + "</option > ";
            });
            $('#ChemicalRange').html(innerHTML);
            $('#ChemicalRange').selectpicker('refresh');
        },
        failure: function (response) { },
        error: function (response) { }
    });
} 

function GetMarketAnalysisRange() {
    var ProductList = "";
    var CountryName = $("#Country option:selected").text();
    //CountryName = CountryName.replace(/ /g, '');
    CountryName = CountryName.trim();
    var regionIDval = $('#Region').val();
    var region = "";
    if (regionIDval == "") {
        var region = "";
    }
    else {
        var region = $("#Region option:selected").text();
    }

    region = $.trim(region);
    ProductList = $('#Product').val();
    // Loading(true);
    $.ajax({
        url: "/LeadManagementSubscription/GetMarketAnalysisRange",
        type: "POST",
        dataType: "json",
        async: true,
        data: "{productlist: '" + ProductList + "', CountryName: '" + CountryName + "',region: '" + region + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (response) { // Loading(false);
            var MarketAnalysisRangeinnerHTML = '';
            $(response.DataList).each(function (jsonIndex, Value) {
                MarketAnalysisRangeinnerHTML += "<option product-value='" + Value.ProductId + "'range-value='" + Value.MarketRangeID + "' >  " + Value.MarketRangeName + "</option > ";
            });
            $('#MarketAnalysisRange').html(MarketAnalysisRangeinnerHTML);
            $('#MarketAnalysisRange').selectpicker('refresh');
        },
        failure: function (response) { },
        error: function (response) { }
    });
} 

$("#ClientCountryName").change(function () {

    var ClientCountryName = $("#ClientCountryName").val();
    if (ClientCountryName == "1" || ClientCountryName == "3") {
        $("#DivState").show();
        GetStateDDL();
    } else {
        $("#DivState").hide();
    }

}); 

function GetStateDDL() {
    var ClientCountryId = $("#ClientCountryName").val();
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetStateDDL",
        data: "{CountryID: '" + ClientCountryId + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var StateNameHTML = '';
            StateNameHTML += "<option value=''>" + "Select Client State" + "</option>";
            $(response).each(function (index, value) {
                if (value.StateName != null || value.StateName != "" || value.StateName != undefined) {
                    StateNameHTML += "<option class='option01' value = '" + value.StateId + "'> " + value.StateName + "</option>"
                }
            });
            $('#StateName').html(StateNameHTML);
        },
        failure: function (response) { },
        error: function (response) { }
    });
}


// Script For Save and Update

function GetRangeCheckbox(elm) {
    var data = []
    $($('#' + elm).find('option:selected')).each(function (idx, val) {
        var innerData = {};
        innerData['PID'] = $(this).attr('product-value');
        innerData['RID'] = $(this).attr('range-value');
        data.push(innerData);
    });
    var rangeCollection = JSON.stringify(data);
    if (rangeCollection == '[]')
        return '';
    else
        return rangeCollection;



}

function GetClintEmail() {
    var ClintEmailinnerHTML = "";
    var PrimaryClintEmailinnerHTML = "";
    var ClintName = "";
    ClintName += "<option value=''>" + "Select Client Name" + "</option>";
    PrimaryClintEmailinnerHTML += "<option value=''>" + "Select Primary Client Email" + "</option>";
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/GetClintEmail",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $(data).each(function (index, value) {
                ClintEmailinnerHTML += "<option value = '" + value.UserId + "'> " + value.CorpEmail + "</option>"
                PrimaryClintEmailinnerHTML += "<option value = '" + value.UserId + "'> " + value.CorpEmail + "</option>"
                ClintName += "<option value = '" + value.Name + "'> " + value.Name + "</option>"
            });

            //$('#ClientName').html(ClintName);
            $('#ClientPrimaryEmail').html(PrimaryClintEmailinnerHTML);
            //$('#SecondaryClientEmails').html(ClintEmailinnerHTML);
            //$('#SecondaryClientEmails').selectpicker('refresh');
          
        },
        failure: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log(response)
        }
    });
}
function isEmail(email) { // Regular expression pattern for email validation
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    // Validate the email against the pattern
    if (emailPattern.test(email)) {
        return true; // Email is valid
    } else {
        return false; // Email is invalid
    }
}
// Add Data in Grid

var arrayOfLead = [];
var Count = 1;
function BindDatainGrid() {
   
    var CountryName = $("#Country option:selected").text();
    // var ProductName = $("#Product option:selected").text().Get().join(",");
    var ProductName = $("#Product option:selected").map(function () {
        return $(this).text();
    }).get().join(',');
   
    //var ClientName = $("#ClientName option:selected").text(); 
    var ClientName = $("#ClientName").val(); 
    var ClientPrimaryEmail = $("#ClientPrimaryEmail option:selected").text();
    var ClientCompanyName = $("#ClientCompanyName").val();
    //var ClientCompanyName = $("#ClientCompanyName option:selected").text();
    var ClientCountryName = $("#ClientCountryName").val();
    var StateName = $("#StateName").val();
    var ClientPhoneNumber = $("#ClientPhoneNumber").val();
    var BillingEmailAddress = $("#BillingEmailAddress").val();
    var SubscriptionType = "Package";//$("#SubscriptionType").val();
    var AssignAnalyst = $("#AssignAnalyst").val();
    // var SecondaryClientEmails = $("#SecondaryClientEmails").val();
    var ClientType = $("#ClientType").val();
    var SecondaryClientEmails = $("#SecondaryClientEmails option:selected").map(function () {
        return $(this).text();
    }).get().join(',');

    var DateFrom = $("#DateFrom").val();
    var DateTo = $("#DateTo").val();

    var Module = $("#Module").val();
    var ModuleName = $("#Module").val();
    var filesval = $("#FileUpload").val();
    var files = "";
    if (filesval == "") { } else {
        files = $("#FileUpload").get(0).files[0].name;
    }
    var PaymentMode = $("#PaymentMode").val();
    var CRM = $("#CRM").val();

    var ModuleName = $("#Module option:selected").map(function () {
        return $(this).text();
    }).get().join(',');
    if (Module != null) {
        Module = $("#Module").val().join(",");
    } else { }
    var SubscriptionRegionCountry = $("#SubscriptionRegionCountry").val();
    var Region = $("#Region").val();
    var RegionName = $("#Region option:selected").map(function () {
        return $(this).text();
    }).get().join(',');
    var Country = $("#Country").val();

    var Product = $("#Product").val();
    if (Product != null) {
        Product = $("#Product").val().join(",");
    } else { }
    var ChemicalRange = $("#ChemicalRange").val();
    var MarketRange = $("#MarketAnalysisRange").val();
    var Remark = $("#Remark").val();
    var UserType = $("#UserType").val();
    var MarketAnalysisRange = "";
    var ChemicalPricingRange = "";
    if (ChemicalRange != "") {
        ChemicalPricingRange = GetRangeCheckbox('ChemicalRange');
    }
    if (MarketRange != "") {
        MarketAnalysisRange = GetRangeCheckbox('MarketAnalysisRange');
    }
    var selectedValues = $("#Module").val();
    if ((ClientCountryName == "1" || ClientCountryName == "3")) { } else {
        $("#StateName").val('');
    }
    if (SubscriptionRegionCountry == "region") {
        $("#Country").val('');
        Country = "";
    } else if (SubscriptionRegionCountry == "country") {
        $("#Region").val('');
        Region = "";
    } else { }
   
    var ModuleName1 = ModuleName.replace(/\s/g, "");
    var RegionName1 = RegionName.replace(/\s/g, "");

    var Trader = "";
    if ($("#Trader").is(':checked')) {
        Trader = $("#Trader").val();
    }
    var Credit = $("#Credit").val();

    var Renew = "";
    if ($("#Renew").is(':checked')) {
        Renew = $("#Renew").val();
    }

    if ((selectedValues.indexOf('9') != -1) && (selectedValues.indexOf('1') == -1) && (selectedValues.indexOf('2') == -1) && (selectedValues.indexOf('3') == -1) && (selectedValues.indexOf('4') == -1) && (selectedValues.indexOf('5') == -1) && (selectedValues.indexOf('6') == -1) && (selectedValues.indexOf('7') == -1) && (selectedValues.indexOf('8') == -1)) {
        Product = "000";
        Region = "000";
        Country = "000";
        SubscriptionRegionCountry = "000";
    }


    if ((ModuleName1 == "CapacityTracker") && (RegionName1 != "Global")) {
        swal("Warning", "Please Select Region is Global", "error");
        return false;
    }
  
    else if (ClientName == "" || ClientName == null || ClientName == undefined)
    {
        swal("Warning", "Please Insert Client Name", "error");
        return false;
    }
    else if (ClientType == "" || ClientType == null || ClientType == undefined) {
        swal("Warning", "Please Select Client Type", "error");
        return false;
    }
    else if (PaymentMode == "" || PaymentMode == null || PaymentMode == undefined) {
        swal("Warning", "Please Select PaymentMode", "error");
        return false;
    }
    else if (ClientPrimaryEmail == "" || ClientPrimaryEmail == null || ClientPrimaryEmail == undefined) {
        swal("Warning", "Please Insert Client Primary Email", "error");
        return false;
    }
    else if (filesval == "" || filesval == null || filesval == undefined) {
        swal("Warning", "Please Upload Payment Document", "error");
        return false;
    }
    else if (ClientCompanyName == "" || ClientCompanyName == null || ClientCompanyName == undefined) {
        swal("Warning", "Please Insert Client Company Name", "error");
        return false;
    } else if (ClientCountryName == "" || ClientCountryName == null || ClientCountryName == undefined) {
        swal("Warning", "Please Insert Client Country Name", "error");
        return false;
    } else if (ClientPhoneNumber == "" || ClientPhoneNumber == null || ClientPhoneNumber == undefined) {
        swal("Warning", "Please Insert Client Phone Number", "error");
        return false;
    } else if (BillingEmailAddress == "" || BillingEmailAddress == null || BillingEmailAddress == undefined) {
        swal("Warning", "Please Insert Billing Email Address", "error");
        return false;
    }
   else if (CRM == "" || CRM == null || CRM == undefined) {
        swal("Warning", "Please Insert CRM.", "error");
        return false;
    } else if (SubscriptionType == "" || SubscriptionType == null || SubscriptionType == undefined) {
        swal("Warning", "Please Insert Subscription Type", "error");
        return false;
    } else if (UserType == "" || UserType == null || UserType == undefined) {
        swal("Warning", "Please Insert User Type", "error");
        return false;
    } else if (AssignAnalyst == "" || AssignAnalyst == null || AssignAnalyst == undefined) {
        swal("Warning", "Please Insert Assign Analyst", "error");
        return false;
    } else if (DateFrom == "" || DateFrom == null || DateFrom == undefined) {
        swal("Warning", "Please Insert Date From", "error");
        return false;
    } else if (DateTo == "" || DateTo == null || DateTo == undefined) {
        swal("Warning", "Please Insert Date To", "error");
        return false;
    } else if (SubscriptionRegionCountry == "" || SubscriptionRegionCountry == null || SubscriptionRegionCountry == undefined) {
        swal("Warning", "Please Insert Subscription Region Country", "error");
        return false;
    } else if ((SubscriptionRegionCountry == "region") && (Region == "" || Region == null || Region == undefined)) {
        swal("Warning", "Please Insert Region", "error");
        return false;

    } else if ((SubscriptionRegionCountry == "country") && (Country == "" || Country == null || Country == undefined)) {
        swal("Warning", "Please Insert Country", "error");
        return false;
    } else if (Product == "" || Product == null || Product == undefined) {
        swal("Warning", "Please Insert Product", "error");
        return false;
    } else if (Module == "" || Module == null || Module == undefined) {
        swal("Warning", "Please Insert Module", "error");
        return false;
    } else if ((selectedValues.indexOf('1') != -1) && (ChemicalRange == "" || ChemicalRange == null || ChemicalRange == undefined)) {
        swal("Warning", "Please Insert ChemicalRange", "error");
        return false;
    }
    else if ((selectedValues.indexOf('2') != -1) && (MarketRange == "" || MarketRange == null || MarketRange == undefined)) {

        swal("Warning", "Please Insert MarketRange", "error");
        return false;

    }
    else if ((selectedValues.indexOf('1') != -1) && (SubscriptionRegionCountry != "country")) {
        swal("Warning", "Please select [Subscription Region/Country] Country Wise!", "error");
        return false;
    }
    else if (Remark == "" || Remark == null || Remark == undefined) {
        swal("Warning", "Please Insert Remark", "error");
        return false;
    } else {
        $('#dvTable').css('display', 'block');
        var tbodyHTML = '';
        tbodyHTML += "<tr id='row" + Count + "'>";
        tbodyHTML += "<td hidden>" + ClientName + "</td>";
        tbodyHTML += "<td>" + ClientPrimaryEmail + "</td>";
        tbodyHTML += "<td hidden>" + ClientCompanyName + "</td>";
        tbodyHTML += "<td hidden>" + ClientCountryName + "</td>";
        tbodyHTML += "<td hidden>" + StateName + "</td>";
        tbodyHTML += "<td hidden>" + ClientPhoneNumber + "</td>";
        tbodyHTML += "<td hidden>" + UserType + "</td>";
        tbodyHTML += "<td hidden>" + ClientType + "</td>"; // Status
        tbodyHTML += "<td hidden>" + BillingEmailAddress + "</td>";
        tbodyHTML += "<td hidden>" + SubscriptionType + "</td>";
        tbodyHTML += "<td hidden>" + AssignAnalyst + "</td>";
        tbodyHTML += "<td hidden>" + SecondaryClientEmails + "</td>";
        tbodyHTML += "<td>" + DateFrom + "</td>";
        tbodyHTML += "<td>" + DateTo + "</td>";
        tbodyHTML += "<td>" + ModuleName + "</td>";
        tbodyHTML += "<td >" + Trader + "</td>";
        tbodyHTML += "<td >" + Credit + "</td>";
        tbodyHTML += "<td >" + Renew + "</td>";

        if (SubscriptionRegionCountry == "region") {
            var RegionName = $("#Region option:selected").text();
            tbodyHTML += "<td>" + RegionName + "</td>";
        } else if (SubscriptionRegionCountry == "country") {
            tbodyHTML += "<td>" + CountryName + "</td>";
        } else { } tbodyHTML += "<td style='text-align:left'>" + "<a onclick=PopupSource('" + encodeURIComponent(ProductName) + "')>" + "Read More" + "</a>" + "</td>"
        // tbodyHTML += "<td>" + ProductName + "</td>";
        tbodyHTML += "<td hidden>" + ChemicalPricingRange + "</td>";
        tbodyHTML += "<td hidden>" + MarketAnalysisRange + "</td>";
        tbodyHTML += "<td hidden>" + Remark + "</td>";
        tbodyHTML += "<td hidden>" + Region + "</td>";
        tbodyHTML += "<td>" + files + "</td>";
        tbodyHTML += "<td>" + PaymentMode + "</td>";
        tbodyHTML += "<td><button class='btn btn-danger' id='remove'>" + "Remove" + "</button></td>";
        tbodyHTML += "</tr>"
        UploadFile();
        var ObjLaed = {
            Id: "row" + Count,
            Name: ClientName,
            CorpEmail: ClientPrimaryEmail,
            Organisation: ClientCompanyName,
            CountryName: "",
            CountryID: ClientCountryName,
            StateName: "",
            StateId: StateName,
            Phone: ClientPhoneNumber,
            module: Module,
            InterestArea: "",
            UserType: UserType,
            Subject: "",
            Query: "", 
           // Status: "New",
            Status: ClientType,
            AssignTo: "",
            Designation: "",
            AssignDate: "",
            IPAddress: "",
            BillingEmail: BillingEmailAddress,
            SubscriptionType: SubscriptionType,
            AssignAnalyst: AssignAnalyst,
            SecondaryClintEmail: SecondaryClientEmails,
            SubscriptionDateFrom: DateFrom,
            SubscriptionDateTo: DateTo,
            Subscription_Country: Country,
            Subscription_Product: Product,
            Chemical_Range: ChemicalPricingRange,
            Market_Analysis_Range: MarketAnalysisRange,
            SubSubscriptionRemark: Remark,
            ProcedureName: "",
            ConnectionTime: "",
            RegionId: Region,
            RegionName: "",
            ModuleId: "",
            ModuleName: "",
            MarketRangeID: "",
            MarketRangeName: "",
            files: files,
            PaymentMode: PaymentMode,
            CRM: CRM,
            Trader: Trader,
            Credit: Credit,
            Renew: Renew
        }

        arrayOfLead.push(ObjLaed);
        Count++
        $('.tbodydata').append(tbodyHTML);


    }
}

function PopupSource(Product) {

    var Product = decodeURIComponent(Product);
    $("#ProductDiscription").html(Product);
    $(".popup-open").css("display", "block");
}

$("#CancelProducts").click(function () {
    $('#ProductDiscription').val('');
    $(".popup-open").css("display", "none");
});

function SaveLeadRecord() {
    $("#SubmitAll").hide();
    $("#loader").show()
    $.ajax({
        type: "POST",
        url: "/LeadManagementSubscription/SaveLeadRecord",
        data: JSON.stringify(arrayOfLead),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var responseMessage = response;
            if (responseMessage == "success")
            {
                $("#loader").hide()
                $('#dvTable').hide();
                swal(
                    { title: "Thank you!", text: "Created Sucessfully.", type: "success" }).then(function () {
                    window.location.reload();
                });
            }
            else {
                $("#loader").hide()
                $("#SubmitAll").show();
                swal({ title: "Failed!", text: "Failed Try Again.", type: "success" }).then(function () { });
            }
        },
        Error: function (ErrMsg) { }
    })

}

$("#Leadlist").on("click", "#remove", function () {

    const row = $(this).closest('tr');
    const indexToRemove = arrayOfLead.findIndex(element => element.Id === row.attr('Id'));

    if (indexToRemove !== -1) { // Remove the object from the array using the splice() method
        arrayOfLead.splice(indexToRemove, 1);

        // Remove the row from the table using the remove() method
        row.remove();
    }
});




function SaveCustomer() {

    var chkbox = $("#chkprimary").prop('checked') ? 1 : 0;
    var Name = $("#Name").val();
    var Email = $("#Email").val();
    var Phone = $("#Phone").val();
    var ClientCompanyId = $("#ClientCompany").val();
    var Designation = $("#Designation").val();

    if (Name == "") {
        $("#Alert").html("Fill Name!");
    }
    else if (Name == "") {
        $("#Alert").html("Fill Email!");
    }
    else if (Name == "") {
        $("#Alert").html("Fill Phone!");
    }
    else if (ClientCompanyId == "") {
        $("#Alert").html("Select Company!");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/LeadManagementSubscription/InsertrecordinSAcustomer",
            data: {
                Name: Name,
                Email: Email,
                Phone: Phone,
                ClientCompanyId: ClientCompanyId,
                chkboxPrimary: chkbox,
                Designation: Designation

            },
            success: function (response) {
                if (response == "Success") {
                    $("#Name").val('');
                    $("#Email").val('');
                    $("#Phone").val('');
                    $("#Alert").html("Created!");
                    //window.location.reload();
                } else {
                    $("#Alert").html(response);
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
}





$("#Register").click(function () {
GetCompany();
    $(".popup-open04").css("display", "block");
});
$("#Cancel").click(function () {
    window.location.reload();
    $(".popup-open04").css("display", "none");
});

//$("#ClientCompanyName").change(function () {

//    var ClintEmailinnerHTML = "";

//       var ClientCompanyName = $("#ClientCompanyName").val();
//        //ClintEmailinnerHTML += "<option value=''>" + "Select Secondary Client Email" + "</option>";
//        $.ajax({
//            type: "POST",
//            url: "/LeadManagementSubscription/GetClintEmailByCompanyId",
//            //contentType: "application/json; charset=utf-8",
//            //dataType: "json",
//            data: { CompanyId: ClientCompanyName},
//            //async: false,
//            success: function (data) {
//                $(data).each(function (index, value) {
//                    ClintEmailinnerHTML += "<option value = '" + value.UserId + "'> " + value.CorpEmail + "</option>"
                  
//                });

//                $('#SecondaryClientEmails').html(ClintEmailinnerHTML);
//                $('#SecondaryClientEmails').selectpicker('refresh');

//            },
//            failure: function (response) {
//                console.log(response)
//            },
//            error: function (response) {
//                console.log(response)
//            }
//        });
    

//});

function GetsecondaryEmail()
{
    var ClintEmailinnerHTML = "";
    
    var ClientCompanyName = $("#ClientPrimaryEmail").val();
        //ClintEmailinnerHTML += "<option value=''>" + "Select Secondary Client Email" + "</option>";
        $.ajax({
            type: "POST",
            url: "/LeadManagementSubscription/GetClintEmailByCompanyId",
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            data: { CompanyId: ClientCompanyName},
            //async: false,
            success: function (data) {
                $(data).each(function (index, value) {
                    ClintEmailinnerHTML += "<option value = '" + value.UserId + "'> " + value.CorpEmail + "</option>"

                });

                $('#SecondaryClientEmails').html(ClintEmailinnerHTML);
                $('#SecondaryClientEmails').selectpicker('refresh');

            },
            failure: function (response) {
                console.log(response)
            },
            error: function (response) {
                console.log(response)
            }
        });
}


function showdivCredit() {
    if ($("#Trader").is(':checked')) {
        $("#divCredit").show();
    }
    else {
        $("#divCredit").hide();
    }


}