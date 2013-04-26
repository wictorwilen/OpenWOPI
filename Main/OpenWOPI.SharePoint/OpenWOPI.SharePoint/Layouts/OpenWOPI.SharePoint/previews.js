function OpenWOPIPreviews() {
    SP.SOD.executeOrDelayUntilScriptLoaded(function () {
        filePreviewManager.previewers.extensionToPreviewerMap.txt = [embeddedWACPreview, WACImagePreview]
        filePreviewManager.previewers.extensionToPreviewerMap.gpx = [embeddedWACPreview, WACImagePreview]
        filePreviewManager.previewers.extensionToPreviewerMap.vsdx = [embeddedWACPreview, WACImagePreview]
        embeddedWACPreview.dimensions.txt = { width: 400, height: 400 }
        embeddedWACPreview.dimensions.gpx = { width: 400, height: 400 }
        embeddedWACPreview.dimensions.vsdx = { width: 200, height: 200 }
    }, "filepreview.js");
    notifyScriptsLoadedAndExecuteWaitingJobs("OpenWOPI.SharePoint/previews.js");
}
OpenWOPIPreviews();