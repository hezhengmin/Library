import moment from 'moment'; //日期時間格式化

export default {
    data() {
        return {
            totalPages: 0,
            totalRecords: 0,
            pageNumber: 1, //第幾頁開始
            pageSize: 10, //每頁10筆
        };
    },
    methods: {
        
    },
    filters: {
        //年月日
        momentTW: function (value) {
            if (!value) return '';
            return moment(value).format('YYYY-MM-DD')
        },
        //只顯示年月
        momentByYYYYMM: function (value) {
            if (!value) return '';
            return moment(value).format('YYYY-MM')
        },
    }
};
