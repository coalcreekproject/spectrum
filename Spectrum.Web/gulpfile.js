/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');
var path = require('path');
var plumber = require('gulp-plumber');

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('sass', function () {
    return gulp.src('./Content/**/*.scss') //('./sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest('./css'));
});

gulp.task('sass:watch', function () {
    //gulp.watch('./sass/**/*.scss', ['sass']);
    gulp.watch('./Content/**/*.scss', ['sass']);
});