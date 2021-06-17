 
/*集中管控首页*/

 var HomePageV8Cef;
    if (!HomePageV8Cef)HomePageV8Cef = {};
    
    (function() {
        // 打开皮带采样机监控界面
        HomePageV8Cef.OpenTrainBeltSampler = function() {
          native function OpenTrainBeltSampler();
          OpenTrainBeltSampler();
      }; 
        
      // 打开火车机械采样机监控界面
        HomePageV8Cef.OpenTrainSampler = function (sampler) {
            native function OpenTrainSampler(sampler);
            OpenTrainSampler(sampler);
      };  

        // 打开全自动制样机监控界面
      HomePageV8Cef.OpenAutoMaker = function() {
        native function OpenAutoMaker();
        OpenAutoMaker();
      };  

        // 打开火车入厂翻车机监控
      HomePageV8Cef.OpenTrainTipper = function() {
        native function OpenTrainTipper();
        OpenTrainTipper();
      };  

        // 打开火车入厂记录查询
      HomePageV8Cef.OpenWeightBridgeLoadToday = function() {
        native function OpenWeightBridgeLoadToday();
        OpenWeightBridgeLoadToday();
      };  

        // 打开汽车入厂重车衡监控
        HomePageV8Cef.OpenTruckWeighter = function (weighter) {
            native function OpenTruckWeighter(weighter);
            OpenTruckWeighter(weighter);
      };  

        // 打开汽车机械采样机监控
        HomePageV8Cef.OpenCarSampler1 = function (sampler) {
            native function OpenCarSampler1(sampler);
            OpenCarSampler1(sampler);
      };   

        // 打开气动传输监控
      HomePageV8Cef.OpenAutoCupboard = function() {
        native function OpenAutoCupboard();
        OpenAutoCupboard();
        };  

        // 打开智能存样柜
        HomePageV8Cef.OpenSampleCabinet = function () {
            native function OpenSampleCabinet();
            OpenSampleCabinet();
        };  

        // 打开化验室监控
      HomePageV8Cef.OpenLaboratory = function() {
        native function OpenLaboratory();
        OpenLaboratory();
      };    

        // 打开合样归批
        HomePageV8Cef.OpenBatchMachine = function () {
            native function OpenBatchMachine();
            OpenBatchMachine();
        };    
    })(); 