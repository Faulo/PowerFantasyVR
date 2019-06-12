@echo off
set name=%1
set csvFile=%2
set zipFile=%3
set trainingTime=%4
set tmpFolder=%TEMP%\%name:~1,-1%
mkdir %tmpFolder%
mlnet auto-train --task multiclass-classification --dataset %csvFile% --label-column-name gesture -x %trainingTime% -o "%tmpFolder%"
copy "%tmpFolder%\SampleMulticlassClassification\SampleMulticlassClassification.Model\MLModel.zip" %zipFile%