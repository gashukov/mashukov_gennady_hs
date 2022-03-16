# mashukov_gennady_hs.git (Unity 2021.1.28f1)
- Проект запускается со сцены Bootstrap.
- Для главного меню используется отдельная сцена. Я понимаю, что в проекте такого размера в этом не было необходимости, но в нормальных проектах это нормально, поэтому сделал так.
- Для генерации точек используется Poisson Disc Sampling. Алгоритм распределяет точки с учетом ограничений и выбирает из них необходимое количество. Если с учетом указанных в LevelStaticData ограничений невозможно сгенерировать заданное количество точек, то в консоль выводится ошибка.
- Конфиг реализован при помощи ScriptableObject. Месторасположение: Assets/Scripts/StaticData
- В проекте используется Extenject.
- Проект рассчитан на ландшафтную ориентацию с соотношением сторон 16x9. Под другие размеры не адаптировал.
![wow](demo.gif)
