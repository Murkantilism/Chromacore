Steps to implement a new level with automation tools:
=====================================================
1) Cut song in FL Studio
2) Add a folder to Resources "LevelX" (ex: Level3)
3) Add pick-up mp3 files to LevelX folder, within another folder "LevelX_pickupTracks"
4) Edit path in TimestampReader.py to match level
5) Run TimestampReader.py to get a textfile of timestamps
6) Edit path in xPositionCalc.cs and NoteAssigner.cs to match level
7) In Unity, check "EditorReset" and "Trigger" booleans for NoteAssigner, AutoNotePlacer, and XPositionCalc
8) Enable XPosition calc, which should run both itself and AutoNotePlacer
9) Use CreatePlatforms, CreateNotes, CreateBackgrounds if needed
10) IF step 8 results in unsorted arrays, as it sometimes can, try the Close & Restart Unity method:

Close & Restart Method:
1) Disable all automation scripts
2) Close & Restart Unity
3) Check Trigger boolean for XpositionCalc
4) Check Trigger and EditorReset booleans for AutomaticNotePlacer
5) Enable XPositionCalc, which should run both itself and AutoNotePlacer