 
/*皮带采样监控界面*/
 
 var TrainBeltSamplerV8Cef;
    if (!TrainBeltSamplerV8Cef)TrainBeltSamplerV8Cef = {};

    (function() {
        // 设置
      TrainBeltSamplerV8Cef.SubmitSet = function(paramSampler1,paramSampler2) {
        native function SubmitSet(paramSampler1,paramSampler2);
        SubmitSet(paramSampler1,paramSampler2);
      };   
      
      //查看故障信息
      TrainBeltSamplerV8Cef.GetHitchs=function(paramSampler){
      native function GetHitchs(paramSampler);
      return GetHitchs(paramSampler);
      };

      //查看采样记录
      TrainBeltSamplerV8Cef.ShowSamplePlan=function(){
      native function ShowSamplePlan();
      ShowSamplePlan();
      };
      
      //查看火车门式采样记录
      TrainBeltSamplerV8Cef.ShowHCSamplePlan=function(){
      native function ShowHCSamplePlan();
      ShowHCSamplePlan();
        };

        // 皮采1解锁牵车
        TrainBeltSamplerV8Cef.LeadCar1 = function () {
            native function LeadCar1();
            LeadCar1();
        };

        // 皮采1解锁皮带
        TrainBeltSamplerV8Cef.MovingBelt1 = function () {
            native function MovingBelt1();
            MovingBelt1();
        };

        // 皮采2解锁牵车
        TrainBeltSamplerV8Cef.LeadCar2 = function () {
            native function LeadCar2();
            LeadCar2();
        };

        // 皮采2解锁皮带
        TrainBeltSamplerV8Cef.MovingBelt2 = function () {
            native function MovingBelt2();
            MovingBelt2();
        };  

        // 皮采1停止采样
        TrainBeltSamplerV8Cef.StopSampler1 = function () {
            native function StopSampler1();
            StopSampler1();
        };  

        // 皮采2停止采样
        TrainBeltSamplerV8Cef.StopSampler2 = function () {
            native function StopSampler2();
            StopSampler2();
        };  

        // 皮采1发送计划
        TrainBeltSamplerV8Cef.SendSampler1 = function () {
            native function SendSampler1();
            SendSampler1();
        };  

        // 皮采2发送计划
        TrainBeltSamplerV8Cef.SendSampler2 = function () {
            native function SendSampler2();
            SendSampler2();
        };  

       // 皮采1报警复位
        TrainBeltSamplerV8Cef.AlarmReset1 = function () {
            native function AlarmReset1();
            AlarmReset1();
        };  

        // 皮采2报警复位
        TrainBeltSamplerV8Cef.AlarmReset2 = function () {
            native function AlarmReset2();
            AlarmReset2();
        };  

        // 皮采1封装机报警复位
        TrainBeltSamplerV8Cef.FZJAlarmReset1 = function () {
            native function FZJAlarmReset1();
            FZJAlarmReset1();
        };  

        // 皮采2封装机报警复位
        TrainBeltSamplerV8Cef.FZJAlarmReset2 = function () {
            native function FZJAlarmReset2();
            FZJAlarmReset2();
        };  

    })(); 