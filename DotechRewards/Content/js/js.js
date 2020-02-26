/*					PAGINATION 
- on change max rows select options fade out all rows gt option value mx = 5
- append pagination list as per numbers of rows / max rows option (20row/5= 4pages )
- each pagination li on click -> fade out all tr gt max rows * li num and (5*pagenum 2 = 10 rows)
- fade out all tr lt max rows * li num - max rows ((5*pagenum 2 = 10) - 5)
- fade in all tr between (maxRows*PageNum) and (maxRows*pageNum)- MaxRows 
*/

function getPagination(table) {
    var lastPage = 1;

    function hideIfLast(self) {
        var isLast = $(self).attr('dr-not');
        if (isLast !== "true") {
            $(self).hide();
        }
        else {
            //alert(isLast);
        }
    }

    function showIfLast(self) {
        //debugger;
        var isLast = $(self).attr('dr-not');
        if (isLast === "true") {
            $(self).show();
        }
        else {
            //alert(isLast);
        }

    }

    function changePage(page) {
        var toChange = $("[dr-change]");
        for (var i = 0; i < toChange.length; i++) {
            var current = toChange[i];
            if (i + 1 == page) {
                $(current).removeClass("weight300");
                $(current).addClass("weight900");
                continue;
            }
            $(current).addClass("weight300");
            $(current).removeClass("weight900");
        }
    }

  $('#maxRows')
    .on('change', function(evt) {
      //$('.paginationprev').html('');						// reset pagination

      lastPage = 1;
      $('.pagination')
        .find('li')
        .slice(1, -1)
        .remove();
      var trnum = 0; // reset tr counter
      var maxRows = 4; // get Max Rows from select option

      if (maxRows == 5000) {
        $('.pagination').hide();
      } else {
        $('.pagination').show();
      }

      var totalRows = $(table + ' tbody tr').length; // numbers of rows
      $(table + ' tr:gt(0)').each(function (a, b, c) {
          console.log(a,b,c);
        // each TR in  table and not the header
        trnum++; // Start Counter
        if (trnum > maxRows) {
          // if tr number gt maxRows

             // fade it out
            hideIfLast(this);
        }
        if (trnum <= maxRows) {
          $(this).show();
        } // else fade in Important in case if it ..
      }); //  was fade out to fade it in
      if (totalRows > maxRows) {
        // if tr total rows gt max rows option
        var pagenum = Math.ceil(totalRows / maxRows); // ceil total(rows/maxrows) to get ..
        //	numbers of pages
        for (var i = 1; i <= pagenum - 1;) {
            var do900 = i ==  1 ? 'weight900' : '';
          // for each page append pagination li
          $('.pagination #prev')
            .before(
              '<li class="d-none d-lg-block"  data-page="' +
                i +
                '">\
								  <span dr-change class="weight300 '+ do900 +'">' +
                i++ +
                '<span class="sr-only d-none">(current)</span></span>\
								</li>'
            )
            .show();
        } // end for i
      } // end if row count > max rows
      $('.pagination [data-page="1"]').addClass('active'); // add active class to the first li
      $('.pagination li').on('click', function(evt) {
        // on click each page
        evt.stopImmediatePropagation();
        evt.preventDefault();
        var pageNum = $(this).attr('data-page'); // get it's number

        var maxRows = parseInt(4); // get Max Rows from select option

        if (pageNum == 'prev') {
          if (lastPage == 1) {
            return;
          }
          pageNum = --lastPage;
        }
        if (pageNum == 'next') {
          if (lastPage == $('.pagination li').length - 2) {
            return;
          }
          pageNum = ++lastPage;
        }

        lastPage = pageNum;

        changePage(pageNum);

        var trIndex = 0; // reset tr counter
        $('.pagination li').removeClass('active'); // remove active class from all li
        $('.pagination [data-page="' + lastPage + '"]').addClass('active'); // add active class to the clicked
        // $(this).addClass('active');					// add active class to the clicked
        limitPagging();
        $(table + ' tr:gt(0)').each(function() {
          // each tr in table not the header
          trIndex++; // tr index counter
          // if tr index gt maxRows*pageNum or lt maxRows*pageNum-maxRows fade if out
          if (
            trIndex > maxRows * pageNum ||
            trIndex <= maxRows * pageNum - maxRows
          ) {
              $(this).hide();
              //debugger;
              showIfLast(this);
          } else {
            $(this).show();
          } //else fade in
        }); // end of for each tr in table
        
      }); // end of on click pagination list
      limitPagging();
      
    })
    .val(4)
    .change();

  // end of on select change

  // END OF PAGINATION
}

function limitPagging() {
  // alert($('.pagination li').length)

  if ($('.pagination li').length > 7) {
    if ($('.pagination li.active').attr('data-page') <= 3) {
      $('.pagination li:gt(5)').hide();
      $('.pagination li:lt(5)').show();
      $('.pagination [data-page="next"]').show();
    }
    if ($('.pagination li.active').attr('data-page') > 3) {
      $('.pagination li:gt(0)').hide();
      $('.pagination [data-page="next"]').show();
      for (
        let i = parseInt($('.pagination li.active').attr('data-page')) - 2;
        i <= parseInt($('.pagination li.active').attr('data-page')) + 2;
        i++
      ) {
        $('.pagination [data-page="' + i-1 + '"]').show();
      }
    }
  }
}

/*$(function() {
  // Just to append id number for each row
  $('table tr:eq(0)').prepend('<th> ID </th>');

  var id = 0;

  $('table tr:gt(0)').each(function() {
    id++;
    $(this).prepend('<td>' + id + '</td>');
  });
});
*/
