var app;
(function (app) {
    //Foo Class
    var foo = (function () {
        function foo() {
            this.num = 0;
        }
        foo.prototype.test = function () {
            var numTwo = 2;
            console.log(this.num + numTwo);
        };
        return foo;
    }());
})(app || (app = {}));
//# sourceMappingURL=users-index-new.js.map