(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-5fe8c902"],{"1f46":function(e,t,n){},"73c1":function(e,t,n){"use strict";n.d(t,"a",(function(){return c}));n("47e2"),n("2241");var i=n("a34a"),a=n.n(i);var r=n("2af9");function s(e,t,n,i,a,r,s){try{var o=e[r](s),c=o.value}catch(u){return void n(u)}o.done?t(c):Promise.resolve(c).then(i,a)}function o(e){return function(){var t=this,n=arguments;return new Promise((function(i,a){var r=e.apply(t,n);function o(e){s(r,i,a,o,c,"next",e)}function c(e){s(r,i,a,o,c,"throw",e)}o(void 0)}))}}var c=function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:function(){};return function(t,n,i){var s=i.value;i.value=o(a.a.mark((function t(){var n,i,o,c=arguments;return a.a.wrap((function(t){while(1)switch(t.prev=t.next){case 0:for(r["d"].loading(),n=c.length,i=new Array(n),o=0;o<n;o++)i[o]=c[o];return t.prev=2,t.next=5,s.call.apply(s,[this].concat(i));case 5:return t.abrupt("return",t.sent);case 8:t.prev=8,t.t0=t["catch"](2),e&&e.call.apply(e,[this,t.t0].concat(i));case 11:return t.prev=11,r["d"].hideLoading(),t.finish(11);case 14:case"end":return t.stop()}}),t,this,[[2,8,11,14]])})))}}},b268:function(e,t,n){"use strict";n.r(t);var i,a,r=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("Layout",{attrs:{"nav-bar-options":e.navBarOptions}},[n("div",{staticClass:"container"},[n("div",{staticClass:"top"},[n("van-swipe",{attrs:{autoplay:3e3,"lazy-render":""}},e._l(e.lifeSpanDetail.desc.images,(function(e,t){return n("van-swipe-item",{key:t},[n("img",{staticClass:"banner",attrs:{src:e}})])})),1),n("div",{staticClass:"content"},[e.isInLifeSapn(e.lifeSpanType)?[n("div",{staticClass:"time-wrapper",domProps:{innerHTML:e._s(e.workTimeText)}}),n("div",{staticClass:"time-wrapper",domProps:{innerHTML:e._s(e.leftTimeText)}})]:e._e(),n("div",{staticClass:"desc"},[e._v(e._s(e.lifeSpanDetail.desc.content))])],2)],1),n("div",{staticClass:"bottom"},["URL"===e.lifeSpanDetail.paramType&&e.hasBuyButton?n("eco-button",{staticClass:"btn-buy",on:{click:function(t){return e.toBuy(e.lifeSpanDetail.url)}}},[e._v(e._s(e.$t("lang_201023_161431_v1HK")))]):"JSON"===e.lifeSpanDetail.paramType&&e.hasBuyButton?n("eco-button",{staticClass:"btn-buy",on:{click:function(t){return e.jsonToBuy(e.lifeSpanDetail.url)}}},[e._v(e._s(e.$t("lang_201023_161431_v1HK")))]):e._e(),"unitCare"===e.lifeSpanType?n("eco-button",{staticClass:"btn-reset",on:{click:e.confirmReset}},[e._v(e._s(e.$t("lang_200703_134637_X7XL")))]):e.isInLifeSapn(e.lifeSpanType)?n("eco-button",{staticClass:"btn-reset",on:{click:e.handleReset}},[e._v(e._s(e.$t("lang_200703_134637_fVxe")))]):e._e()],1)])])},s=[],o=n("a34a"),c=n.n(o),u=n("2f62"),l=n("0530"),p=n("73c1"),f=n("2af9"),h=n("60fe");function d(e,t,n,i,a,r,s){try{var o=e[r](s),c=o.value}catch(u){return void n(u)}o.done?t(c):Promise.resolve(c).then(i,a)}function v(e){return function(){var t=this,n=arguments;return new Promise((function(i,a){var r=e.apply(t,n);function s(e){d(r,i,a,s,o,"next",e)}function o(e){d(r,i,a,s,o,"throw",e)}s(void 0)}))}}function _(e,t,n,i,a){var r={};return Object.keys(i).forEach((function(e){r[e]=i[e]})),r.enumerable=!!r.enumerable,r.configurable=!!r.configurable,("value"in r||r.initializer)&&(r.writable=!0),r=n.slice().reverse().reduce((function(n,i){return i(e,t,n)||n}),r),a&&void 0!==r.initializer&&(r.value=r.initializer?r.initializer.call(a):void 0,r.initializer=void 0),void 0===r.initializer&&(Object.defineProperty(e,t,r),r=null),r}function b(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);t&&(i=i.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,i)}return n}function y(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?b(Object(n),!0).forEach((function(t){m(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):b(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function m(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}var g={components:{EcoButton:f["a"]},data:function(){return{navBarOptions:{title:null},lifeSpanDetail:{images:[]},lifeSpanType:"",hasBuyButton:!1}},computed:y(y({},Object(u["d"])(["robotInfo","lifeSpan","purchaseList"])),{},{workTimeText:function(){var e=this,t=this.lifeSpan.find((function(t){return t.type===e.lifeSpanType}))||{},n=t.left,i=void 0===n?0:n,a=t.total,r=void 0===a?0:a;if("dustBag"===this.lifeSpanType)return this.$t("lang_210701_093023_dW0t").replace("[number]","<span>".concat(r-i,"</span>"));var s=Math.ceil(r/60),o=s-Math.ceil(i/60);return this.$t("lang_200703_134638_fi1g").replace("[number]","<span>".concat(o,"</span>"))},leftTimeText:function(){var e=this,t=this.lifeSpan.find((function(t){return t.type===e.lifeSpanType}))||{},n=t.left,i=void 0===n?0:n;if("dustBag"===this.lifeSpanType)return this.$t("lang_210701_093023_PK4o").replace("[number]","<span>".concat(i,"</span>"));var a=Math.ceil(i/60);return this.$t("lang_200703_134638_Z3ie").replace("[number]","<span>".concat(a,"</span>"))}}),created:function(){try{var e=this.$route.params,t=e.type,n=e.url;this.hasBuyButton=void 0!=n,this.lifeSpanType=t,this.lifeSpanDetail=this.purchaseList.find((function(e){return e.type===t})),this.navBarOptions.title=this.lifeSpanDetail.name,this.sendBigData(0)}catch(i){console.log(i)}},methods:(i=Object(p["a"])(),a=y(y({},Object(u["c"])(["SET_LIFE_SPAN"])),{},{toBuy:function(e){Object(l["navigateTo"])(e)},jsonToBuy:function(e){try{var t=JSON.parse(e);console.log(t),Object(l["k"])(t),this.sendBigData(2,t.params.goodsId)}catch(n){console.log(n)}},handleReset:function(){var e=this;this.sendBigData(1);var t={sideBrush:this.$t("side_brush"),brush:this.$t("roll_brush"),heap:this.$t("filter"),dustBag:this.$t("lang_210701_092932_f9jN"),roundMop:this.$t("lang_201229_085744_iL8h")};if("unitCare"!==this.lifeSpanType){var n=this.$t("lang_210317_110309_j1yx");n=n.replace("[string]",t[this.lifeSpanType]),this.$dialog.confirm({message:n,confirmButtonText:this.$t("common_confirm"),cancelButtonText:this.$t("common_cannel")}).then((function(){e.confirmReset()}))}else this.confirmReset()},confirmReset:function(){var e=this;return v(c.a.mark((function t(){var n;return c.a.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.next=2,Object(l["m"])("resetLifeSpan",{type:e.lifeSpanType});case 2:n=e.lifeSpan.map((function(t){return y(y({},t),{},{left:t.type===e.lifeSpanType?t.total:t.left})})),e.SET_LIFE_SPAN(n);case 4:case"end":return t.stop()}}),t)})))()},isInLifeSapn:function(e){var t=!1;return console.log(e),console.log(this.lifeSpan),this.lifeSpan.map((function(n){n.type===e&&(t=!0)})),t},sendBigData:function(e,t){var n,i=this.$route.params,a=i.type,r=i.percent,s=i.left,o=i.pageTitle,c=["放心使用","中度损耗","重度损耗","清理脏污","尘袋已满"],u={sideBrush:"边刷",brush:"滚刷",heap:"滤芯",roundMop:"拖布",unitCare:"尘袋",dustBag:"部件保养"},p=u[a],f=parseInt(s/60)+"";"sideBrush"===a||"brush"===a||"heap"===a||"roundMop"===a?n=r>=40?c[0]:r>=15?c[1]:c[2]:"unitCare"===a?n=r>=15?c[0]:c[3]:"dustBag"===a?(n=s>=3?c[0]:c[4],f=s+""):(n="未知",p="未知"),0===e?Object(l["a"])(h["c"].ROBOT_SUPPLY_INFO,{loss_degree:n,page_title:"1"===o?"耗材列表页":"设置列表页",consumables_types:p}):1===e?Object(l["a"])(h["c"].ROBOT_SUPPLY_RESET,{consumables_remainder:f,consumables_types:p}):2===e&&Object(l["a"])(h["c"].ROBOT_SUPPLY_BUY,{loss_degree:n,page_title:"耗材详情页",consumables_types:p,consumables_remainder:f,consumables_id:t})}}),_(a,"confirmReset",[i],Object.getOwnPropertyDescriptor(a,"confirmReset"),a),a)},O=g,S=(n("df8c7"),n("2877")),T=Object(S["a"])(O,r,s,!1,null,"614e0d52",null);t["default"]=T.exports},df8c7:function(e,t,n){"use strict";n("1f46")}}]);