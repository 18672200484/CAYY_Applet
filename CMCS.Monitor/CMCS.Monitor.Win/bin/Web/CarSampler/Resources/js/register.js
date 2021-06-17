 
/*汽车衡监控界面*/
 
 var CarSamplerV8Cef;
    if (!CarSamplerV8Cef)CarSamplerV8Cef = {};

    (function() {
        // 急停
        CarSamplerV8Cef.Stop = function (paramSampler) {
        native function Stop(paramSampler);
        return Stop(paramSampler);
        }; 

        // 复位
        CarSamplerV8Cef.Reset = function (paramSampler) {
            native function Reset(paramSampler);
            return Reset(paramSampler);
        }; 

        // 制样急停
        CarSamplerV8Cef.ZYStop = function (paramSampler) {
            native function ZYStop(paramSampler);
            return ZYStop(paramSampler);
        };

        // 制样复位
        CarSamplerV8Cef.ZYReset = function (paramSampler) {
            native function ZYReset(paramSampler);
            return ZYReset(paramSampler);
        }; 
      
        // 车辆信息
      CarSamplerV8Cef.CarInfo = function(paramSampler) {
        native function CarInfo(paramSampler);
        CarInfo(paramSampler);
      };   
      
        // 故障复位
      CarSamplerV8Cef.ErrorReset = function(paramSampler) {
        native function ErrorReset(paramSampler);
        ErrorReset(paramSampler);
      };   
      
        // 采样历史记录
      CarSamplerV8Cef.SampleHistory = function(paramSampler) {
        native function SampleHistory(paramSampler);
        SampleHistory(paramSampler);
      };   

      //查看故障信息
      CarSamplerV8Cef.GetHitchs=function(paramSampler){
      native function GetHitchs(paramSampler);
      return GetHitchs(paramSampler);
      };

      //切换采样机选中
      CarSamplerV8Cef.ChangeSelected=function(paramSampler){
          native function ChangeSelected(paramSampler);
          return ChangeSelected(paramSampler);
        };

        // 打开视频预览窗体
        CarSamplerV8Cef.OpenHikVideo = function (param) {
            native function OpenHikVideo(param);
            OpenHikVideo(param);
        };

    })(); 