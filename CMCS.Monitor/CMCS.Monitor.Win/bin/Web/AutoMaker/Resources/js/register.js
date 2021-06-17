 
/*全自动制样机监控界面*/
 
 var AutoMakerV8Cef;
    if (!AutoMakerV8Cef)AutoMakerV8Cef = {};

    (function() {
      
      //查看故障信息
      AutoMakerV8Cef.GetHitchs=function(paramSampler){
      native function GetHitchs(paramSampler);
      return GetHitchs(paramSampler);
        };

        // 打开报警信息
        AutoMakerV8Cef.ErrorInfo = function (param) {
            native function ErrorInfo(param);
            ErrorInfo(param);
        };

        // 历史故障
        AutoMakerV8Cef.FaultRecord = function (param) {
            native function FaultRecord(param);
            FaultRecord(param);
        };

        // 故障复位
        AutoMakerV8Cef.FaultReset = function (param) {
            native function FaultReset(param);
            FaultReset(param);
        };
    })(); 