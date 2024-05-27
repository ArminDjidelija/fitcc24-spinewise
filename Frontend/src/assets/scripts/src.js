function init_plugin(){
  $(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
      $('#sidebar').toggleClass('active');
      if(!upaljen && window.innerWidth<=768)
        upaljen=!upaljen;
    });
  });

  var upaljen=false;
  $(document).ready(function () {
    $('#sidebarCollapse1').on('click', function () {
      upaljen=!upaljen;
      $('#sidebar').toggleClass('active');
    });
  });
  $(document).ready(function () {
    $('.liClick').on('click', function () {
      if(upaljen && window.innerWidth<=768){
        $('#sidebar').toggleClass('active');
        upaljen=!upaljen;
      }
    });
  });
}
