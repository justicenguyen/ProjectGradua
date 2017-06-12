
    $('.hienthi').off('click').on('click', function () {
    var idsp = $(this).data('idsp');
        $.ajax(
            {
        type: 'GET',
                url: "/SanPham/ThayDoiHienThi",
                data: {"idsp": idsp },
                success: function(rs)
                {
        alert(rs);
    }

            })
});

$('.spbanchay').off('click').on('click', function () {
    var idsp = $(this).data('idsp');
    $.ajax(
        {
            type: 'GET',
            url: "/SanPham/ThayDoiSanPhamBanChay",
            data: { "idsp": idsp },
            success: function (rs) {
                alert(rs);
            }

        })
});

