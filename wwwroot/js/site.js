$(document).ready(function () {
    $('#btnPrint').click(function () {
        var printContents = document.getElementById('detailes').innerHTML;
        var originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        location.reload();
    });
    $('#accountSelect').select2({
        placeholder: "-- Select Account --",
        allowClear: true,
        width: '100%',
        matcher: function (params, data) {
            if ($.trim(params.term) === '') return data;
            if (data.text.toLowerCase().indexOf(params.term.toLowerCase()) > -1) return data;
            return null;
        }
    });
    $("form").on("submit", function (e) {
        var accountId = $("#accountSelect").val();
        if (!accountId) {
            e.preventDefault();
            toastr.warning("Please choose an account first", "Warning");
            return false;
        }
        toastr.info("Fetching data, please wait...", "Loading");
    });
    $('#detailsModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');

        $("#detailsContainer").html("<p>Loading...</p>");
        toastr.info("Fetching data, please wait...", "Loading");

        $.get("/Accounts/GetTransactionDetails", { id: id }, function (data) {
            $("#detailsContainer").html(data);
            toastr.success("Details uploaded successfully", "Success");
        }).fail(function () {
            toastr.error("An error occurred while loading the details", "Error");
        });
    });
    $('#btnExport').click(function (e) {
        var selectedAccount = $('#accountSelect').val();
        if (!selectedAccount) {
            toastr.warning('Please select an account before exporting!');
            e.preventDefault();
            return false;
        }
        var hasData = $('#detailes table tbody tr').length > 0;
        if (!hasData) {
            toastr.info('No data to export!');
            e.preventDefault();
            return false;
        }
        else {
            toastr.info("Fetching data, please wait...", "Loading");
        }
    });
});