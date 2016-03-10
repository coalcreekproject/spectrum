'use strict';

/**
 * HOMER - Responsive Admin Theme
 * Copyright 2015 Webapplayers.com
 *
 * Sweet Alert Directive
 * Official plugin - http://tristanedwards.me/sweetalert
 * Angular implementation inspiring by https://github.com/oitozero/ngSweetAlert
 */

function sweetAlert($timeout, $window) {
    var _swal = $window.swal;
    return {
        swal: function swal(arg1, arg2, arg3) {
            $timeout(function () {
                if (typeof arg2 === 'function') {
                    _swal(arg1, function (isConfirm) {
                        $timeout(function () {
                            arg2(isConfirm);
                        });
                    }, arg3);
                } else {
                    _swal(arg1, arg2, arg3);
                }
            }, 200);
        },
        success: function success(title, message) {
            $timeout(function () {
                _swal(title, message, 'success');
            }, 200);
        },
        error: function error(title, message) {
            $timeout(function () {
                _swal(title, message, 'error');
            }, 200);
        },
        warning: function warning(title, message) {
            $timeout(function () {
                _swal(title, message, 'warning');
            }, 200);
        },
        info: function info(title, message) {
            $timeout(function () {
                _swal(title, message, 'info');
            }, 200);
        }

    };
};

/**
 * Pass function into module
 */
angular.module('app').factory('sweetAlert', sweetAlert);
