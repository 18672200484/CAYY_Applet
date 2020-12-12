
/*火车机械采样机监控界面*/

var TrainSamplerV8Cef;
if (!TrainSamplerV8Cef) TrainSamplerV8Cef = {};

(function () {

    //切换采样机选中
    TrainSamplerV8Cef.ChangeSelected = function (paramSampler) {
        native function ChangeSelected(paramSampler);
        return ChangeSelected(paramSampler);
    };

})(); 