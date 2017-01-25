module.exports = function (grunt) {
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
        typescript: {
            base: {
                src: ['Scripts/app/core/typescript/*.ts'],
                dest: 'Scripts/app/core/typescript',
                options: {
                    module: 'amd', //or commonjs 
                    target: 'es5', //or es3 
                    basePath: 'path/to/typescript/files',
                    sourceMap: true,
                    declaration: true
                }
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
                        dest: "Scripts/dist/development",
                        cwd: "Scripts"
                    }
                ]
            }
        },
        uglify: {
            production: {
                files: {
                    'Scripts/dist/production/all.min.js': [
                        "Scripts/dist/development/controllers/main.js",
                        "Scripts/dist/development/directives/*.js",
                        "Scripts/dist/development/factories/*.js",
                        "Scripts/dist/development/filters/*.js",
                        "Scripts/dist/development/spectrum-core/*.js",
                        "Scripts/dist/development/spectrum-eoc/*.js"
                    ],

                    'Scripts/dist/production/all.vendor.js': [
                        "Scripts/bootstrap.js",
                        "Scripts/charts.js",
                        "Scripts/homer.js"
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
};