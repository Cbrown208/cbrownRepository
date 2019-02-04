
import createNumberMask from 'text-mask-addons/dist/createNumberMask';

export class FormaterConstants {

    public static numberMask = createNumberMask({
        prefix: '$',
        suffix: ''
    });

    public static decimalNumberMask = createNumberMask({
        prefix: '',
        suffix: '', // This will put the dollar sign at the end.
        allowDecimal: true,
        decimalLimit: 2,
        allowLeadingZeroes: false
    });

    public static percentageMask = createNumberMask({
        prefix: '',
        suffix: '%', // This will put the % sign at the end.
        includeThousandsSeparator: false,
        allowLeadingZeroes: false,
        integerLimit: 2
    });

    static unMaskValue = (eventValue: string) => {
        let obj = eventValue.replace(/([$%()_{}, ])+/g, '').replace(/^(-)+|(-)+$/g, '');
        return obj;
    }

    static formatNumber = (unformatedNumber: any) => {
        if (unformatedNumber == null || unformatedNumber == "") {
            return ".00";
        }
        if (unformatedNumber.indexOf(".") == -1  && !(unformatedNumber % 1 !== 0)) {
            return unformatedNumber + ".00";
        }
        return unformatedNumber;
    }

}