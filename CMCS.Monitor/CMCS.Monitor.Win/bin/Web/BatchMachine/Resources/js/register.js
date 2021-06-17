 
/*全自动制样机监控界面*/
 
var BatchMachineV8Cef;
if (!BatchMachineV8Cef)BatchMachineV8Cef = {};

    (function() {
      
      //查看故障信息
      BatchMachineV8Cef.GetHitchs=function(paramSampler){
      native function GetHitchs(paramSampler);
      return GetHitchs(paramSampler);
        };

        // 打开报警信息
        BatchMachineV8Cef.ErrorInfo = function (param) {
            native function ErrorInfo(param);
            ErrorInfo(param);
        };

        // 倒料命令
        BatchMachineV8Cef.SendDLCMD = function (param) {
            native function SendDLCMD(param);
            SendDLCMD(param);
        };

        // 归批命令
        BatchMachineV8Cef.SendGPCMD = function (param) {
            native function SendGPCMD(param);
            SendGPCMD(param);
        };

        // 卸车刷卡
        BatchMachineV8Cef.UnloadSwip = function (param) {
            native function UnloadSwip(param);
            UnloadSwip(param);
        };

        // 装车刷卡
        BatchMachineV8Cef.TruckUnloadSwip = function (param) {
            native function TruckUnloadSwip(param);
            TruckUnloadSwip(param);
        };

        // 历史故障
        BatchMachineV8Cef.FaultRecord = function (param) {
            native function FaultRecord(param);
            FaultRecord(param);
        };

        // 故障复位
        BatchMachineV8Cef.FaultReset = function (param) {
            native function FaultReset(param);
            FaultReset(param);
        };
    })(); 