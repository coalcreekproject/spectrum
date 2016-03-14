module.exports = function(grunt) {
    "use strict";

    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        less: {
            development: {
                files: {
                    "Content/style-less.css": "Less/style.less" // Not replacing style.css until we are ready
                }
            }
        },
        jshint: {
            options: {
                jshintrc: ".jshintrc"
            },
            files: [
                "Gruntfile.js",
                "Scripts/controllers/*.js",
                "Scripts/directives/*.js",
                "Scripts/factories/*.js",
                "Scripts/filters/*.js",
                "Scripts/spectrum-core/*.js",
                "Scripts/spectrum-eoc/*.js",
                "Scripts/app.js"
            ]
        },
        copy: {
            main: {
                files: [

                    // makes all src relative to cwd
                    { expand: true, cwd: "Scripts/app/", src: ["**"], dest: "Content/dist/development" }
                ]
            }
        },
        // Must be run before the uglify step, otherwise the uglifier chokes on ES2015 syntax
        babel: {
            options: {
                sourceMap: false,
                presets: ["es2015"]
            },
            development: {
                files: [
                    {
                        expand: true,
                        src: "**/*.js",
                        dest: "Content/dist/development",
                        cwd: "Content/dist/development"
                    }
                ]
            }
        },
        concat: {
            options: {
                separator: ";"
            },
            production: {
                files: {
                    'Content/dist/production/all.min.js': [
                        "Content/dist/development/app.js",
                        "Content/dist/development/controllers/main.js",
                        "Content/dist/development/core/*.js",
                        "Content/dist/development/data/*.js",
                        "Content/dist/development/directives/*.js",
                        "Content/dist/development/eoc/*.js",
                        "Content/dist/development/factories/*.js",
                        "Content/dist/development/filters/*.js"
                    ],

                    'Content/dist/production/all.vendor.min.js': [
                        "bower_components/jquery/dist/jquery.js",
                        "bower_components/jquery-flot/jquery.flot.js",
                        "bower_components/jquery.flot.spline/index.js",
                        "bower_components/sweetalert/dist/sweetalert-dev.js",
                        "bower_components/summernote/dist/summernote.js",
                        "bower_components/bootstrap/dist/js/bootstrap.js",
                        "bower_components/angular/angular.js",
                        "bower_components/angular-animate/angular-animate.js",
                        "bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                        "bower_components/angular-bootstrap-tour/dist/angular-bootstrap-tour.js",
                        "bower_components/angular-drag-and-drop-lists/angular-drag-and-drop-lists.js",
                        "bower_components/angular-flot/angular-flot.js",
                        "bower_components/angular-notify/angular-notify.js",
                        "bower_components/angular-peity/angular-peity.js",
                        "bower_components/angular-route/angular-route.js",
                        "bower_components/angular-sanitize/angular-sanitize.js",
                        "bower_components/angular-summernote/dist/angular-summernote.js",
                        "bower_components/angular-sweetalert/SweetAlert.js",
                        "bower_components/angular-ui/build/angular-ui.js",
                        "bower_components/angular-ui-bootstrap/index.js",
                        "Scripts/charts.js",
                        "Scripts/homer.js"
                    ]
                }
            }
        },
        uglify: {
            production: {
                files: {
                    'Content/dist/production/all.min.js': [
                        "Content/dist/production/all.min.js"
                    ],
                    'Content/dist/production/all.vendor.min.js': [
                        "Content/dist/production/all.vendor.min.js"
                    ]
                }
            }
        },
        watch: {
            js: {
                files: [
                    "Gruntfile.js",
                    "Scripts/controllers/*.js",
                    "Scripts/directives/*.js",
                    "Scripts/factories/*.js",
                    "Scripts/filters/*.js",
                    "Scripts/spectrum-core/*.js",
                    "Scripts/spectrum-eoc/*.js",
                    "Scripts/app.js"
                ],
                tasks: ["jshint"]
            },
            styles: {
                files: ["Less/*.less"],
                tasks: ["less"]
            }
        }
    });

    // Load all plug-in tasks automatically
    require("load-grunt-tasks")(grunt);

    // Default task - excluding watch task as you may not want this by default
    grunt.registerTask("default", ["less", "jshint"]);

    // Production build task - transpile, concat files, and minify
    grunt.registerTask("build-prod", ["copy", "babel", "concat", "uglify"]);
};