"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var createNumberMask_1 = require("text-mask-addons/dist/createNumberMask");
var FormaterConstants = /** @class */ (function () {
    function FormaterConstants() {
    }
    FormaterConstants.numberMask = createNumberMask_1.default({
        prefix: '$',
        suffix: ''
    });
    FormaterConstants.decimalNumberMask = createNumberMask_1.default({
        prefix: '',
        suffix: '',
        allowDecimal: true,
        decimalLimit: 2,
        allowLeadingZeroes: false
    });
    FormaterConstants.percentageMask = createNumberMask_1.default({
        prefix: '',
        suffix: '%',
        includeThousandsSeparator: false,
        allowLeadingZeroes: false,
        integerLimit: 2
    });
    FormaterConstants.unMaskValue = function (eventValue) {
        var obj = eventValue.replace(/([$%()_{}, ])+/g, '').replace(/^(-)+|(-)+$/g, '');
        return obj;
    };
    FormaterConstants.formatNumber = function (unformatedNumber) {
        if (unformatedNumber == null || unformatedNumber == "") {
            return ".00";
        }
        if (unformatedNumber.indexOf(".") == -1 && !(unformatedNumber % 1 !== 0)) {
            return unformatedNumber + ".00";
        }
        return unformatedNumber;
    };
    return FormaterConstants;
}());
exports.FormaterConstants = FormaterConstants;

//# sourceMappingURL=formater.constants.js.map
