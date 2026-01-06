@echo off
echo.
echo Введите namespace для всех файлов (например: CarParkingChaos.ECS.System):
set /p NS=
echo.
echo Обрабатываю все .cs файлы в текущей папке и подпапках...
echo.

set COUNT=0

for /r %%f in (*.cs) do (
    echo Файл: %%f
    
    rem Проверяем есть ли уже namespace
    findstr /B "namespace " "%%f" >nul
    if errorlevel 1 (
        rem Создаем временный файл
        (
            rem Копируем using директивы
            findstr /B "using " "%%f"
            
            echo.
            
            rem Добавляем namespace
            echo namespace %NS%
            echo {
            
            rem Копируем остальной код
            findstr /V /B "using " "%%f"
            
            rem Закрываем namespace
            echo }
        ) > "%%f.tmp"
        
        rem Заменяем оригинальный файл
        move /y "%%f.tmp" "%%f" >nul
        
        set /a COUNT+=1
        echo   [✓ Добавлен namespace]
    ) else (
        echo   [⏭️ Уже имеет namespace]
    )
    echo.
)

echo.
echo ================================
echo РЕЗУЛЬТАТЫ:
echo ================================
echo Обработано файлов: %COUNT%
echo Namespace: %NS%
echo.
echo Нажмите любую клавишу для выхода...
pause >nul