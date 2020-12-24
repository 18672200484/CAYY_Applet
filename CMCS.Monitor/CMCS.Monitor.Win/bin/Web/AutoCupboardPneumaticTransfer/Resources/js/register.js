 
/*智能存样柜及气动传输*/
 
 var AutoCupboardV8Cef;
    if (!AutoCupboardV8Cef)AutoCupboardV8Cef = {};

    (function() {
      //查看故障信息
        AutoCupboardV8Cef.ShowHitchs=function(){
            native function ShowHitchs();
            ShowHitchs();
      };

      //查看传输记录
        AutoCupboardV8Cef.ShowCygControlCmd=function(){
          native function ShowCygControlCmd();
          ShowCygControlCmd();
      };
    })(); 