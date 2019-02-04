Chart.pluginService.register({
    afterDraw: function(chart) {
        try {
            var width = chart.chart.width,
                height = chart.chart.height,
                ctx = chart.chart.ctx,
                centerConfig = chart.options.center;

            ctx.restore();
            ctx.font = "1.0em" + " " + centerConfig.font;
            if (innerWidth <= 320) {
                ctx.font = "0.88em" + " " + centerConfig.font;
            }
            if (innerWidth >= 768 && innerWidth <= 1024) {
                ctx.font = "0.88em" + " " + centerConfig.font;
            }

            ctx.fillStyle = centerConfig.fillStyle;
            ctx.textBaseline = "middle";

            var textX = Math.round((width - ctx.measureText(centerConfig.text).width) / 2),
                textY = (height + 20) / 2;

            ctx.fillText(centerConfig.text, textX, textY);
            ctx.save();

        } catch (e) {
            console.log("Unable to render center label in dognut chart");
            console.log(e);
        }
    }
});