# Changelog

## 1.1.0.0 — 2026-06-09

**New**
- Full support for the new **lamedb5 (Enigma2 v5)** format — open, edit and save.
- **Per-profile lamedb version** selector (ver. 3 / ver. 4 / ver. 5) so each receiver is written in its own format on upload (default ver. 4).
- lamedb5 is recognized when opening files, ZIP/RAR archives and over FTP; the stale lamedb/lamedb5 file is cleaned up automatically on upload.
- Added IPTV stream service types: **5001, 5002, 8193, 8739** (in addition to 4097).
- Added HD/UHD service types: **AVC Stereo HDTV, HEVC, HEVC UHD**.
- Service alternatives, hidden bouquets and SPACE spacers are now preserved when saving.
- Corrected DVB-T modulation and DVB-S2 roll-off value labels.

**Fixed**
- Crash when saving settings.
- Interface language change not being applied / saved correctly.
- Task list getting duplicated after opening the Options dialog.
- Crash when a profile password could not be decrypted.
- Missing "Add profile" button and the lamedb dropdown not enabling/defaulting correctly.
- Removed the unused "I have donated" checkbox.
