# WhatsApp Automation (C# .NET & CefSharp & WA-JS & SQL 2008 R2)

A desktop Windows Forms application in C# that embeds WhatsApp Web via **CefSharp** and uses **WPPConnect WA-JS** to automate sending WhatsApp messages from **Microsoft SQL Server 2008 R2** database records in the background.

---

## Features

### 1. SQL Server 2008 R2 Integration
- **Persistent DB Config**:
  - Automatically loads and saves database settings in `dbconfig.txt`:
    - Line 1: SQL Server Instance (e.g. `DELL\SQL2008R2`)
    - Line 2: Database Name (e.g. `_CODE_`)
  - Fixed Credentials configured:

  - "Test Connection" button to verify SQL connectivity directly from the UI.

### 2. Automated Sales Messaging Queue (Every 10 Seconds)
- **Automatic Polling**:
  - Automatically polls `tbSalesDetails` every 10 seconds.
  - Matches today's sales (or a custom specific date):
    ```sql
    SELECT SI, VoucherNo, CustomerName, CustomerMobile, GrandTotal, SoldDate 
    FROM tbSalesDetails 
    WHERE CAST(SoldDate AS DATE) = CAST(GETDATE() AS DATE)
      AND InvoiceType = 'Normal' 
      AND VoucherPrefix = (SELECT TOP 1 SettingsValue FROM tbSettings WHERE SettingsName = 'VoucherPrefix')
      AND (TokenNumber = '' OR TokenNumber IS NULL) 
      AND CustomerMobile IS NOT NULL 
      AND LTRIM(RTRIM(CustomerMobile)) <> ''
    ORDER BY SI ASC
    ```
- **Phone Number Normalization**:
  - Strips symbols/spaces.
  - Automatically prepends `91` to 10-digit mobile numbers (e.g., `9567366327` -> `919567366327`).
- **Personalized Message Template**:
  ```text
  Hai {CustomerName}
  Thank you for purchase !
  Your bill amount is {GrandTotal}
  ```
- **Database Update**:
  - Immediately marks sent records: `UPDATE tbSalesDetails SET TokenNumber = '1' WHERE SI = @SI`.
- **Anti-Flood & Batch Pause**:
  - When rows are available, the 10-second timer pauses while processing the batch.
  - Waits 2.5 seconds between each message to prevent WhatsApp rate limiting.
  - Automatically resumes the 10-second timer once the batch is finished.

### 3. Settings Tab & Customizable Templates
- **Dynamic Message Template**:
  - Configure your own custom message template with placeholder tags:
    - `{CustomerName}` : Customer name from `tbSalesDetails`
    - `{GrandTotal}` : Formatted grand total (e.g. 312.00)
    - `{VoucherNo}` : Voucher / Bill number
    - `{SoldDate}` : Bill date formatted (e.g. 22-Sep-2026)
    - `{NetTotal}` : Net total before discount
    - `{Discount}` : Discount amount
    - `{ShopName}` : Shop / Business header name
  - Saved to and loaded from `appsettings.json`.

### 4. Automated Bill / Invoice PDF Generation
- **PDF Generation via iTextSharp**:
  - Checkbox: `[x] Send Bill / Invoice PDF with WhatsApp message`.
  - Generates beautiful, professional invoices on-the-fly and sends them directly via WhatsApp with the personalized message template as the caption.
  - Paper Format Options:
    - **A4 Invoice**: Standard full-page invoice with headers, customer details, itemized table, totals, and thank you footer.
    - **3-inch Thermal (80mm)**: Compact POS receipt layout for thermal printers.
  - Shop Header Configuration:
    - `Header 1`: Store / Business Name
    - `Header 2`: Address / Tagline
    - `Header 3`: Contact & GSTIN
  - Item Details Query (with `WITH (NOLOCK)` for zero POS locking):
    ```sql
    SELECT tbSales.ItemName, tbSales.Qty, tbSales.Rate, tbSales.Disc, tbSales.NetAmount 
    FROM tbSalesDetails WITH (NOLOCK) 
    INNER JOIN tbSales WITH (NOLOCK) 
       ON tbSales.VoucherNo = tbSalesDetails.VoucherNo 
      AND tbSales.InvoiceType = tbSalesDetails.InvoiceType 
      AND tbSales.VoucherPrefix = tbSalesDetails.VoucherPrefix 
    WHERE tbSalesDetails.SI = @SI
    ```

### 5. Background Execution (Minimize to System Tray)
- Minimizing the application hides the window to the Windows notification tray (`NotifyIcon`).
- The background timer continues checking SQL and sending messages unattended.
- Double-clicking the tray icon restores the window.
- Right-click tray menu allows starting/stopping auto-send or exiting.

### 6. Manual Send Tab
- Easily test sending single messages or documents (PDF, Word, Excel, Images) with captions to any mobile number manually.

### 7. Application Software Updates (Inside Settings)
- **One-Click Update Button**: Users can click **🔄 Check App Update** in the Settings tab.
- **Auto-check on Startup**: Automatically checks for new versions when the app starts.
- **Easy Developer Releases**: Supports both GitHub Releases and custom JSON manifests (`version.json`).
- **Seamless Self-Update**:
  - Displays download progress with real-time percentage and file size.
  - Automatically closes the app, swaps the new `.exe` (or extracts full `.zip` update packages), and restarts the app with zero manual file copying.

---

## How Developers Push Updates (Easy Guide)

### Method A: Via GitHub Releases (Recommended & Free)
1. Build the updated version in Visual Studio (e.g. increment `<Version>1.0.1</Version>` in `WhatsappAutomation.csproj`).
2. Go to your GitHub repository -> **Releases** -> **Draft a new release**.
3. Set tag name: `v1.0.1` (e.g. `v1.0.1`).
4. Type your release notes / bug fixes in the description.
5. Attach `WhatsappAutomation.exe` (or a `.zip` containing all updated files) to the release.
6. Publish the release.
7. In the app's Settings -> **Update Source**, set your repo: `yourusername/yourrepo` (e.g. `safvan/whatsapp-automation`) and click **Save Settings**.
8. Click **🔄 Check App Update**. The app will detect `v1.0.1`, show the release notes, download the new executable, and restart automatically!

### Method B: Via Custom Server or Direct URL (`version.json`)
1. Host a `version.json` file on any web server or cloud storage (see [version.json.template](file:///g:/AI%20Projects/Whatsapp%20Automation/version.json.template)):
   ```json
   {
     "version": "1.0.1",
     "downloadUrl": "https://myserver.com/updates/WhatsappAutomation.exe",
     "fileName": "WhatsappAutomation.exe",
     "releaseNotes": "Fixed SQL connection bugs and improved speed."
   }
   ```
2. In Settings -> **Update Source**, enter your URL: `https://myserver.com/updates/version.json` and click **Save Settings**.
3. When you release a bug fix, update the version number and executable file on your server.


## How to Run

### In Visual Studio 2019:
1. Open [WhatsappAutomation.sln](file:///g:/AI%20Projects/Whatsapp%20Automation/WhatsappAutomation.sln).
2. Set platform to **`x64`**.
3. Press **F5**.

### Standalone Executable:
Run the compiled executable directly:
```text
bin\x64\Debug\net48\WhatsappAutomation.exe
```

---

## How to Use Auto-Send
1. Launch the app and log into WhatsApp Web on the right panel.
2. Under the **SQL Auto-Send** tab:
   - Check your SQL Instance (`DELL\SQL2008R2`) and Database (`_CODE_`).
   - Click **Test Connection** to confirm connectivity.
3. Click **▶ Start Auto-Send (Every 10s)**:
   - The app will begin polling `tbSalesDetails` every 10 seconds.
   - Any new sale will automatically receive a WhatsApp message and have its `TokenNumber` updated to `'1'`.
4. Minimize the window anytime to let it run in the background.
