let _sortingId = "name";
let _isDesc = false;
let _fname = null;

$(document).ready(function checkPage() {
    if ($('#tbody1 tr').length != 10) {
        $('ul li').last().prev().addClass('disabled');
        $('ul li').last().addClass('disabled');
        if (!$('ul li a:contains("2")').parent().hasClass('active')) {
            $('ul li a:contains("2")').parent().addClass('disabled');
        }

    } else {
        $('ul li').last().prev().removeClass('disabled');
        $('ul li').last().removeClass('disabled');
        $('ul li a:contains("2")').parent().removeClass('disabled');
    }
});

function nextPage() {
    let curPage = parseInt($('ul li.active a').text(), 10);

    if ($('ul li.active').children().text() == 1) {
        $('ul li.active').removeClass('active');
        $('ul li a:contains("2")').parent().addClass('active');
        $('ul li').first().removeClass('disabled');
        updateData(curPage + 1);
    }
    else {
        $('ul li.active').children().text(curPage + 1);
        $('ul li').last().prev().children().text(curPage + 2);
        $('ul li').first().next().children().text(curPage);
        updateData(curPage + 1);
    }
};

function prevPage() {
    let curPage = parseInt($('ul li.active a').text(), 10);
    if (curPage > 2) {
        $('ul li.active').children().text(curPage - 1);
        $('ul li').last().prev().children().text(curPage);
        $('ul li').first().next().children().text(curPage - 2);
        updateData(curPage - 1);
    }
    if (curPage == 2 && $('ul li.active').children().text() != 1) {
        $('ul li.active').removeClass('active');
        $('ul li a:contains("1")').parent().addClass('active');
        $('ul li').first().addClass('disabled');
        updateData(curPage - 1);
    }

}



function sortingBy(e) {
    let sortedBy = $(e).attr("id");
    if (_sortingId == sortedBy && _isDesc == false) {
        _isDesc = true;

    } else if (_sortingId == sortedBy && _isDesc == true) {
        _isDesc = false;
    }
    _sortingId = sortedBy;
    if (_isDesc) {
        $('div.sortIcon').removeClass('glyphicon glyphicon-sort-by-attributes-alt');
        $('div.sortIcon').removeClass('glyphicon glyphicon-sort-by-attributes');
        $(e).parent().children().last().removeClass('glyphicon glyphicon-sort-by-attributes');
        $(e).parent().children().last().addClass('glyphicon glyphicon-sort-by-attributes-alt');
    } else if (!_isDesc) {
        $('div.sortIcon').removeClass('glyphicon glyphicon-sort-by-attributes-alt');
        $('div.sortIcon').removeClass('glyphicon glyphicon-sort-by-attributes');
        $(e).parent().children().last().removeClass('glyphicon glyphicon-sort-by-attributes-alt');
        $(e).parent().children().last().addClass('glyphicon glyphicon-sort-by-attributes');
    }
    let curPage = parseInt($('ul li.active a').text(), 10);
    updateData(curPage, sortedBy, _isDesc);
}

function findByName() {
    let searchData = $('#searchData').val() == "" ? null : $('#searchData').val();
    let curPage = parseInt($('ul li.active a').text(), 10);
    _fname = searchData;
    updateData(curPage, _sortingId, _isDesc, searchData);
}

function updateData(pageNum, sortBy, isDesc, fname) {
    if (sortBy === undefined) {
        sortBy = _sortingId;
    }
    if (isDesc === undefined) {
        isDesc = _isDesc;
    }
    if (fname === undefined) {
        fname = _fname;
    }
    $.ajax(
        {
            type: "POST",
            url: '',
            success: function () {
                $("#tbody1").load("@Url.Action("GetUserList", "Home")?pageNum=" + pageNum + "&sortBy=" + sortBy + "&isDesc=" + isDesc + "&fName=" + fname);
            }
        })
};