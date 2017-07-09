namespace System.Windows.Forms
{
    using Accessibility;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Internal;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms.Layout;

    [DefaultEvent("Click"), Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ComVisible(true), ClassInterface(ClassInterfaceType.AutoDispatch), ToolboxItemFilter("System.Windows.Forms"), DefaultProperty("Text"), DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class Control : Component, System.Windows.Forms.UnsafeNativeMethods.IOleControl, System.Windows.Forms.UnsafeNativeMethods.IOleObject, System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject, System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject, System.Windows.Forms.UnsafeNativeMethods.IOleWindow, System.Windows.Forms.UnsafeNativeMethods.IViewObject, System.Windows.Forms.UnsafeNativeMethods.IViewObject2, System.Windows.Forms.UnsafeNativeMethods.IPersist, System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit, System.Windows.Forms.UnsafeNativeMethods.IPersistPropertyBag, System.Windows.Forms.UnsafeNativeMethods.IPersistStorage, System.Windows.Forms.UnsafeNativeMethods.IQuickActivate, ISupportOleDropSource, IDropTarget, ISynchronizeInvoke, IWin32Window, IArrangedElement, IBindableComponent, IComponent, IDisposable
    {
        // Events
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnAutoSizeChangedDescr"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler AutoSizeChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventAutoSizeChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventAutoSizeChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnBackColorChangedDescr")]
        public event EventHandler BackColorChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventBackColor, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventBackColor, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnBackgroundImageChangedDescr")]
        public event EventHandler BackgroundImageChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventBackgroundImage, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventBackgroundImage, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnBackgroundImageLayoutChangedDescr")]
        public event EventHandler BackgroundImageLayoutChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventBackgroundImageLayout, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventBackgroundImageLayout, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnBindingContextChangedDescr")]
        public event EventHandler BindingContextChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventBindingContext, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventBindingContext, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnCausesValidationChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler CausesValidationChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventCausesValidation, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventCausesValidation, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnChangeUICuesDescr"), System.Windows.Forms.SRCategory("CatBehavior")]
        public event UICuesEventHandler ChangeUICues
        {
            add
            {
                base.Events.AddHandler(Control.EventChangeUICues, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventChangeUICues, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatAction"), System.Windows.Forms.SRDescription("ControlOnClickDescr")]
        public event EventHandler Click
        {
            add
            {
                base.Events.AddHandler(Control.EventClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventClick, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnClientSizeChangedDescr")]
        public event EventHandler ClientSizeChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventClientSize, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventClientSize, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnContextMenuChangedDescr"), Browsable(false)]
        public event EventHandler ContextMenuChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventContextMenu, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventContextMenu, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlContextMenuStripChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler ContextMenuStripChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventContextMenuStrip, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventContextMenuStrip, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlOnControlAddedDescr"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event ControlEventHandler ControlAdded
        {
            add
            {
                base.Events.AddHandler(Control.EventControlAdded, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventControlAdded, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnControlRemovedDescr"), System.Windows.Forms.SRCategory("CatBehavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event ControlEventHandler ControlRemoved
        {
            add
            {
                base.Events.AddHandler(Control.EventControlRemoved, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventControlRemoved, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnCursorChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler CursorChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventCursor, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventCursor, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnDockChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler DockChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventDock, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDock, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatAction"), System.Windows.Forms.SRDescription("ControlOnDoubleClickDescr")]
        public event EventHandler DoubleClick
        {
            add
            {
                base.Events.AddHandler(Control.EventDoubleClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDoubleClick, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnDragDropDescr"), System.Windows.Forms.SRCategory("CatDragDrop")]
        public event DragEventHandler DragDrop
        {
            add
            {
                base.Events.AddHandler(Control.EventDragDrop, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDragDrop, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnDragEnterDescr"), System.Windows.Forms.SRCategory("CatDragDrop")]
        public event DragEventHandler DragEnter
        {
            add
            {
                base.Events.AddHandler(Control.EventDragEnter, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDragEnter, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnDragLeaveDescr"), System.Windows.Forms.SRCategory("CatDragDrop")]
        public event EventHandler DragLeave
        {
            add
            {
                base.Events.AddHandler(Control.EventDragLeave, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDragLeave, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatDragDrop"), System.Windows.Forms.SRDescription("ControlOnDragOverDescr")]
        public event DragEventHandler DragOver
        {
            add
            {
                base.Events.AddHandler(Control.EventDragOver, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventDragOver, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnEnabledChangedDescr")]
        public event EventHandler EnabledChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventEnabled, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventEnabled, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnEnterDescr"), System.Windows.Forms.SRCategory("CatFocus")]
        public event EventHandler Enter
        {
            add
            {
                base.Events.AddHandler(Control.EventEnter, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventEnter, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnFontChangedDescr")]
        public event EventHandler FontChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventFont, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventFont, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnForeColorChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler ForeColorChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventForeColor, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventForeColor, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnGiveFeedbackDescr"), System.Windows.Forms.SRCategory("CatDragDrop")]
        public event GiveFeedbackEventHandler GiveFeedback
        {
            add
            {
                base.Events.AddHandler(Control.EventGiveFeedback, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventGiveFeedback, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnGotFocusDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatFocus")]
        public event EventHandler GotFocus
        {
            add
            {
                base.Events.AddHandler(Control.EventGotFocus, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventGotFocus, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPrivate"), System.Windows.Forms.SRDescription("ControlOnCreateHandleDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler HandleCreated
        {
            add
            {
                base.Events.AddHandler(Control.EventHandleCreated, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventHandleCreated, value);
            }
        }
        [EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatPrivate"), System.Windows.Forms.SRDescription("ControlOnDestroyHandleDescr"), Browsable(false)]
        public event EventHandler HandleDestroyed
        {
            add
            {
                base.Events.AddHandler(Control.EventHandleDestroyed, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventHandleDestroyed, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnHelpDescr"), System.Windows.Forms.SRCategory("CatBehavior")]
        public event HelpEventHandler HelpRequested
        {
            add
            {
                base.Events.AddHandler(Control.EventHelpRequested, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventHelpRequested, value);
            }
        }
        [WinCategory("Behavior"), System.Windows.Forms.SRDescription("ControlOnImeModeChangedDescr")]
        public event EventHandler ImeModeChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventImeModeChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventImeModeChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatAppearance"), System.Windows.Forms.SRDescription("ControlOnInvalidateDescr"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public event InvalidateEventHandler Invalidated
        {
            add
            {
                base.Events.AddHandler(Control.EventInvalidated, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventInvalidated, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatKey"), System.Windows.Forms.SRDescription("ControlOnKeyDownDescr")]
        public event KeyEventHandler KeyDown
        {
            add
            {
                base.Events.AddHandler(Control.EventKeyDown, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventKeyDown, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnKeyPressDescr"), System.Windows.Forms.SRCategory("CatKey")]
        public event KeyPressEventHandler KeyPress
        {
            add
            {
                base.Events.AddHandler(Control.EventKeyPress, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventKeyPress, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatKey"), System.Windows.Forms.SRDescription("ControlOnKeyUpDescr")]
        public event KeyEventHandler KeyUp
        {
            add
            {
                base.Events.AddHandler(Control.EventKeyUp, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventKeyUp, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlOnLayoutDescr")]
        public event LayoutEventHandler Layout
        {
            add
            {
                base.Events.AddHandler(Control.EventLayout, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventLayout, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnLeaveDescr"), System.Windows.Forms.SRCategory("CatFocus")]
        public event EventHandler Leave
        {
            add
            {
                base.Events.AddHandler(Control.EventLeave, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventLeave, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnLocationChangedDescr")]
        public event EventHandler LocationChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventLocation, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventLocation, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnLostFocusDescr"), System.Windows.Forms.SRCategory("CatFocus"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public event EventHandler LostFocus
        {
            add
            {
                base.Events.AddHandler(Control.EventLostFocus, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventLostFocus, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlOnMarginChangedDescr")]
        public event EventHandler MarginChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventMarginChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMarginChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatAction"), System.Windows.Forms.SRDescription("ControlOnMouseCaptureChangedDescr")]
        public event EventHandler MouseCaptureChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseCaptureChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseCaptureChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatAction"), System.Windows.Forms.SRDescription("ControlOnMouseClickDescr")]
        public event MouseEventHandler MouseClick
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseClick, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnMouseDoubleClickDescr"), System.Windows.Forms.SRCategory("CatAction")]
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseDoubleClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseDoubleClick, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnMouseDownDescr"), System.Windows.Forms.SRCategory("CatMouse")]
        public event MouseEventHandler MouseDown
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseDown, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseDown, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatMouse"), System.Windows.Forms.SRDescription("ControlOnMouseEnterDescr")]
        public event EventHandler MouseEnter
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseEnter, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseEnter, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnMouseHoverDescr"), System.Windows.Forms.SRCategory("CatMouse")]
        public event EventHandler MouseHover
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseHover, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseHover, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnMouseLeaveDescr"), System.Windows.Forms.SRCategory("CatMouse")]
        public event EventHandler MouseLeave
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseLeave, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseLeave, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnMouseMoveDescr"), System.Windows.Forms.SRCategory("CatMouse")]
        public event MouseEventHandler MouseMove
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseMove, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseMove, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatMouse"), System.Windows.Forms.SRDescription("ControlOnMouseUpDescr")]
        public event MouseEventHandler MouseUp
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseUp, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseUp, value);
            }
        }
        [Browsable(false), System.Windows.Forms.SRCategory("CatMouse"), System.Windows.Forms.SRDescription("ControlOnMouseWheelDescr"), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event MouseEventHandler MouseWheel
        {
            add
            {
                base.Events.AddHandler(Control.EventMouseWheel, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMouseWheel, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlOnMoveDescr")]
        public event EventHandler Move
        {
            add
            {
                base.Events.AddHandler(Control.EventMove, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventMove, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlOnPaddingChangedDescr")]
        public event EventHandler PaddingChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventPaddingChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventPaddingChanged, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnPaintDescr"), System.Windows.Forms.SRCategory("CatAppearance")]
        public event PaintEventHandler Paint
        {
            add
            {
                base.Events.AddHandler(Control.EventPaint, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventPaint, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnParentChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler ParentChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventParent, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventParent, value);
            }
        }
        [System.Windows.Forms.SRDescription("PreviewKeyDownDescr"), System.Windows.Forms.SRCategory("CatKey")]
        public event PreviewKeyDownEventHandler PreviewKeyDown
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)] add
            {
                base.Events.AddHandler(Control.EventPreviewKeyDown, value);
            }
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)] remove
            {
                base.Events.RemoveHandler(Control.EventPreviewKeyDown, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnQueryAccessibilityHelpDescr"), System.Windows.Forms.SRCategory("CatBehavior")]
        public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add
            {
                base.Events.AddHandler(Control.EventQueryAccessibilityHelp, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventQueryAccessibilityHelp, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnQueryContinueDragDescr"), System.Windows.Forms.SRCategory("CatDragDrop")]
        public event QueryContinueDragEventHandler QueryContinueDrag
        {
            add
            {
                base.Events.AddHandler(Control.EventQueryContinueDrag, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventQueryContinueDrag, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlRegionChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler RegionChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventRegionChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventRegionChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlOnResizeDescr"), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler Resize
        {
            add
            {
                base.Events.AddHandler(Control.EventResize, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventResize, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnRightToLeftChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler RightToLeftChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventRightToLeft, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventRightToLeft, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnSizeChangedDescr")]
        public event EventHandler SizeChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventSize, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventSize, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlOnStyleChangedDescr")]
        public event EventHandler StyleChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventStyleChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventStyleChanged, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnSystemColorsChangedDescr"), System.Windows.Forms.SRCategory("CatBehavior")]
        public event EventHandler SystemColorsChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventSystemColorsChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventSystemColorsChanged, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnTabIndexChangedDescr")]
        public event EventHandler TabIndexChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventTabIndex, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventTabIndex, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnTabStopChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler TabStopChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventTabStop, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventTabStop, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatPropertyChanged"), System.Windows.Forms.SRDescription("ControlOnTextChangedDescr")]
        public event EventHandler TextChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventText, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventText, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnValidatedDescr"), System.Windows.Forms.SRCategory("CatFocus")]
        public event EventHandler Validated
        {
            add
            {
                base.Events.AddHandler(Control.EventValidated, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventValidated, value);
            }
        }
        [System.Windows.Forms.SRCategory("CatFocus"), System.Windows.Forms.SRDescription("ControlOnValidatingDescr")]
        public event CancelEventHandler Validating
        {
            add
            {
                base.Events.AddHandler(Control.EventValidating, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventValidating, value);
            }
        }
        [System.Windows.Forms.SRDescription("ControlOnVisibleChangedDescr"), System.Windows.Forms.SRCategory("CatPropertyChanged")]
        public event EventHandler VisibleChanged
        {
            add
            {
                base.Events.AddHandler(Control.EventVisible, value);
            }
            remove
            {
                base.Events.RemoveHandler(Control.EventVisible, value);
            }
        }

        // Methods
        static Control()
        {
            Control.EventAutoSizeChanged = new object();
            Control.EventKeyDown = new object();
            Control.EventKeyPress = new object();
            Control.EventKeyUp = new object();
            Control.EventMouseDown = new object();
            Control.EventMouseEnter = new object();
            Control.EventMouseLeave = new object();
            Control.EventMouseHover = new object();
            Control.EventMouseMove = new object();
            Control.EventMouseUp = new object();
            Control.EventMouseWheel = new object();
            Control.EventClick = new object();
            Control.EventClientSize = new object();
            Control.EventDoubleClick = new object();
            Control.EventMouseClick = new object();
            Control.EventMouseDoubleClick = new object();
            Control.EventMouseCaptureChanged = new object();
            Control.EventMove = new object();
            Control.EventResize = new object();
            Control.EventLayout = new object();
            Control.EventGotFocus = new object();
            Control.EventLostFocus = new object();
            Control.EventEnabledChanged = new object();
            Control.EventEnter = new object();
            Control.EventLeave = new object();
            Control.EventHandleCreated = new object();
            Control.EventHandleDestroyed = new object();
            Control.EventVisibleChanged = new object();
            Control.EventControlAdded = new object();
            Control.EventControlRemoved = new object();
            Control.EventChangeUICues = new object();
            Control.EventSystemColorsChanged = new object();
            Control.EventValidating = new object();
            Control.EventValidated = new object();
            Control.EventStyleChanged = new object();
            Control.EventImeModeChanged = new object();
            Control.EventHelpRequested = new object();
            Control.EventPaint = new object();
            Control.EventInvalidated = new object();
            Control.EventQueryContinueDrag = new object();
            Control.EventGiveFeedback = new object();
            Control.EventDragEnter = new object();
            Control.EventDragLeave = new object();
            Control.EventDragOver = new object();
            Control.EventDragDrop = new object();
            Control.EventQueryAccessibilityHelp = new object();
            Control.EventBackgroundImage = new object();
            Control.EventBackgroundImageLayout = new object();
            Control.EventBindingContext = new object();
            Control.EventBackColor = new object();
            Control.EventParent = new object();
            Control.EventVisible = new object();
            Control.EventText = new object();
            Control.EventTabStop = new object();
            Control.EventTabIndex = new object();
            Control.EventSize = new object();
            Control.EventRightToLeft = new object();
            Control.EventLocation = new object();
            Control.EventForeColor = new object();
            Control.EventFont = new object();
            Control.EventEnabled = new object();
            Control.EventDock = new object();
            Control.EventCursor = new object();
            Control.EventContextMenu = new object();
            Control.EventContextMenuStrip = new object();
            Control.EventCausesValidation = new object();
            Control.EventRegionChanged = new object();
            Control.EventMarginChanged = new object();
            Control.EventPaddingChanged = new object();
            Control.EventPreviewKeyDown = new object();
            Control.mouseWheelMessage = 0x20a;
            Control.checkForIllegalCrossThreadCalls = Debugger.IsAttached;
            Control.inCrossThreadSafeCall = false;
            Control.currentHelpInfo = null;
            Control.PropName = PropertyStore.CreateKey();
            Control.PropBackBrush = PropertyStore.CreateKey();
            Control.PropFontHeight = PropertyStore.CreateKey();
            Control.PropCurrentAmbientFont = PropertyStore.CreateKey();
            Control.PropControlsCollection = PropertyStore.CreateKey();
            Control.PropBackColor = PropertyStore.CreateKey();
            Control.PropForeColor = PropertyStore.CreateKey();
            Control.PropFont = PropertyStore.CreateKey();
            Control.PropBackgroundImage = PropertyStore.CreateKey();
            Control.PropFontHandleWrapper = PropertyStore.CreateKey();
            Control.PropUserData = PropertyStore.CreateKey();
            Control.PropContextMenu = PropertyStore.CreateKey();
            Control.PropCursor = PropertyStore.CreateKey();
            Control.PropThreadCallbackList = PropertyStore.CreateKey();
            Control.PropRegion = PropertyStore.CreateKey();
            Control.PropCharsToIgnore = PropertyStore.CreateKey();
            Control.PropRightToLeft = PropertyStore.CreateKey();
            Control.PropImeMode = PropertyStore.CreateKey();
            Control.PropUnrestrictedImeMode = PropertyStore.CreateKey();
            Control.PropUpdatingCachedIme = PropertyStore.CreateKey();
            Control.PropBindings = PropertyStore.CreateKey();
            Control.PropBindingManager = PropertyStore.CreateKey();
            Control.PropAccessibleDefaultActionDescription = PropertyStore.CreateKey();
            Control.PropAccessibleDescription = PropertyStore.CreateKey();
            Control.PropAccessibility = PropertyStore.CreateKey();
            Control.PropNcAccessibility = PropertyStore.CreateKey();
            Control.PropAccessibleName = PropertyStore.CreateKey();
            Control.PropAccessibleRole = PropertyStore.CreateKey();
            Control.PropPaintingException = PropertyStore.CreateKey();
            Control.PropActiveXImpl = PropertyStore.CreateKey();
            Control.PropControlVersionInfo = PropertyStore.CreateKey();
            Control.PropBackgroundImageLayout = PropertyStore.CreateKey();
            Control.PropAccessibleHelpProvider = PropertyStore.CreateKey();
            Control.PropContextMenuStrip = PropertyStore.CreateKey();
            Control.PropAutoScrollOffset = PropertyStore.CreateKey();
            Control.PropUseCompatibleTextRendering = PropertyStore.CreateKey();
            Control.PropCacheTextCount = PropertyStore.CreateKey();
            Control.PropCacheTextField = PropertyStore.CreateKey();
            Control.PropAmbientPropertiesService = PropertyStore.CreateKey();
            Control.UseCompatibleTextRenderingDefault = false;
            Control.WM_GETCONTROLNAME = System.Windows.Forms.SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLNAME");
            Control.WM_GETCONTROLTYPE = System.Windows.Forms.SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLTYPE");
        }

        public Control() : this(true)
        {
        }

        internal Control(bool autoInstallSyncContext)
        {
            this.propertyStore = new PropertyStore();
            this.window = new ControlNativeWindow(this);
            this.RequiredScalingEnabled = true;
            this.RequiredScaling = BoundsSpecified.All;
            this.tabIndex = -1;
            this.state = 0x2000e;
            this.state2 = 8;
            this.SetStyle(ControlStyles.UseTextForAccessibility | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.StandardDoubleClick | (ControlStyles.Selectable | (ControlStyles.StandardClick | ControlStyles.UserPaint)))), true);
            this.InitMouseWheelSupport();
            if (this.DefaultMargin != CommonProperties.DefaultMargin)
            {
                this.Margin = this.DefaultMargin;
            }
            if (this.DefaultMinimumSize != CommonProperties.DefaultMinimumSize)
            {
                this.MinimumSize = this.DefaultMinimumSize;
            }
            if (this.DefaultMaximumSize != CommonProperties.DefaultMaximumSize)
            {
                this.MaximumSize = this.DefaultMaximumSize;
            }
            System.Drawing.Size size1 = this.DefaultSize;
            this.width = size1.Width;
            this.height = size1.Height;
            CommonProperties.xClearPreferredSizeCache(this);
            if ((this.width != 0) && (this.height != 0))
            {
                int num1;
                int num2;
                int num3;
                System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
                rect1.bottom = num1 = 0;
                rect1.top = num2 = num1;
                rect1.right = num3 = num2;
                rect1.left = num3;
                System.Windows.Forms.CreateParams params1 = this.CreateParams;
                System.Windows.Forms.SafeNativeMethods.AdjustWindowRectEx(ref rect1, params1.Style, false, params1.ExStyle);
                this.clientWidth = this.width - (rect1.right - rect1.left);
                this.clientHeight = this.height - (rect1.bottom - rect1.top);
            }
            if (autoInstallSyncContext)
            {
                Application.ThreadContext.FromCurrent().InstallWindowsFormsSyncContextIfNeeded();
            }
        }

        public Control(string text) : this(null, text)
        {
        }

        public Control(Control parent, string text) : this()
        {
            this.Parent = parent;
            this.Text = text;
        }

        public Control(string text, int left, int top, int width, int height) : this(null, text, left, top, width, height)
        {
        }

        public Control(Control parent, string text, int left, int top, int width, int height) : this(parent, text)
        {
            this.Location = new Point(left, top);
            this.Size = new System.Drawing.Size(width, height);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal void AccessibilityNotifyClients(AccessibleEvents accEvent, int childID)
        {
            this.AccessibilityNotifyClients(accEvent, -4, childID);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int objectID, int childID)
        {
            if (this.IsHandleCreated)
            {
                System.Windows.Forms.UnsafeNativeMethods.NotifyWinEvent((int) accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
            }
        }

        private IntPtr ActiveXMergeRegion(IntPtr region)
        {
            return this.ActiveXInstance.MergeRegion(region);
        }

        private void ActiveXOnFocus(bool focus)
        {
            this.ActiveXInstance.OnFocus(focus);
        }

        private void ActiveXUpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
        {
            this.ActiveXInstance.UpdateBounds(ref x, ref y, ref width, ref height, flags);
        }

        private void ActiveXViewChanged()
        {
            this.ActiveXInstance.ViewChangedInternal();
        }

        internal virtual void AddReflectChild()
        {
        }

        internal virtual Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
        {
            System.Drawing.Size size1 = LayoutUtils.ConvertZeroToUnbounded(this.MaximumSize);
            Rectangle rectangle1 = new Rectangle(suggestedX, suggestedY, 0, 0);
            rectangle1.Size = LayoutUtils.IntersectSizes(new System.Drawing.Size(proposedWidth, proposedHeight), size1);
            rectangle1.Size = LayoutUtils.UnionSizes(rectangle1.Size, this.MinimumSize);
            return rectangle1;
        }

        internal System.Drawing.Size ApplySizeConstraints(System.Drawing.Size proposedSize)
        {
            Rectangle rectangle1 = this.ApplyBoundsConstraints(0, 0, proposedSize.Width, proposedSize.Height);
            return rectangle1.Size;
        }

        internal System.Drawing.Size ApplySizeConstraints(int width, int height)
        {
            Rectangle rectangle1 = this.ApplyBoundsConstraints(0, 0, width, height);
            return rectangle1.Size;
        }

        internal virtual void AssignParent(Control value)
        {
            if (value != null)
            {
                this.RequiredScalingEnabled = value.RequiredScalingEnabled;
            }
            if (this.CanAccessProperties)
            {
                System.Drawing.Font font1 = this.Font;
                System.Drawing.Color color1 = this.ForeColor;
                System.Drawing.Color color2 = this.BackColor;
                System.Windows.Forms.RightToLeft left1 = this.RightToLeft;
                bool flag1 = this.Enabled;
                bool flag2 = this.Visible;
                this.parent = value;
                this.OnParentChanged(EventArgs.Empty);
                if (this.GetAnyDisposingInHierarchy())
                {
                    return;
                }
                if (flag1 != this.Enabled)
                {
                    this.OnEnabledChanged(EventArgs.Empty);
                }
                bool flag3 = this.Visible;
                if ((flag2 != flag3) && ((flag2 || !flag3) || ((this.parent != null) || this.GetTopLevel())))
                {
                    this.OnVisibleChanged(EventArgs.Empty);
                }
                if (!font1.Equals(this.Font))
                {
                    this.OnFontChanged(EventArgs.Empty);
                }
                if (!color1.Equals(this.ForeColor))
                {
                    this.OnForeColorChanged(EventArgs.Empty);
                }
                if (!color2.Equals(this.BackColor))
                {
                    this.OnBackColorChanged(EventArgs.Empty);
                }
                if (left1 != this.RightToLeft)
                {
                    this.OnRightToLeftChanged(EventArgs.Empty);
                }
                if ((this.Properties.GetObject(Control.PropBindingManager) == null) && this.Created)
                {
                    this.OnBindingContextChanged(EventArgs.Empty);
                }
            }
            else
            {
                this.parent = value;
                this.OnParentChanged(EventArgs.Empty);
            }
            this.SetState(0x1000000, false);
            if (this.ParentInternal != null)
            {
                this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.All);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IAsyncResult BeginInvoke(Delegate method)
        {
            return this.BeginInvoke(method, null);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IAsyncResult BeginInvoke(Delegate method, params object[] args)
        {
            IAsyncResult result1;
            using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
            {
                Control control1 = this.FindMarshalingControl();
                result1 = (IAsyncResult) control1.MarshaledInvoke(this, method, args, false);
            }
            return result1;
        }

        internal void BeginUpdateInternal()
        {
            if (this.IsHandleCreated)
            {
                if (this.updateCount == 0)
                {
                    this.SendMessage(11, 0, 0);
                }
                this.updateCount = (short) (this.updateCount + 1);
            }
        }

        public void BringToFront()
        {
            if (this.parent != null)
            {
                this.parent.Controls.SetChildIndex(this, 0);
            }
            else if ((this.IsHandleCreated && this.GetTopLevel()) && System.Windows.Forms.SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle)))
            {
                System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), System.Windows.Forms.NativeMethods.HWND_TOP, 0, 0, 0, 0, 3);
            }
        }

        internal virtual bool CanProcessMnemonic()
        {
            if (!this.Enabled || !this.Visible)
            {
                return false;
            }
            if (this.parent != null)
            {
                return this.parent.CanProcessMnemonic();
            }
            return true;
        }

        internal virtual bool CanSelectCore()
        {
            if ((this.controlStyle & ControlStyles.Selectable) != ControlStyles.Selectable)
            {
                return false;
            }
            for (Control control1 = this; control1 != null; control1 = control1.parent)
            {
                if (!control1.Enabled || !control1.Visible)
                {
                    return false;
                }
            }
            return true;
        }

        internal static void CheckParentingCycle(Control bottom, Control toFind)
        {
            Form form1 = null;
            Control control1 = null;
            for (Control control2 = bottom; control2 != null; control2 = control2.ParentInternal)
            {
                control1 = control2;
                if (control2 == toFind)
                {
                    throw new ArgumentException(System.Windows.Forms.SR.GetString("CircularOwner"));
                }
            }
            if ((control1 != null) && (control1 is Form))
            {
                Form form2 = (Form) control1;
                for (Form form3 = form2; form3 != null; form3 = form3.OwnerInternal)
                {
                    form1 = form3;
                    if (form3 == toFind)
                    {
                        throw new ArgumentException(System.Windows.Forms.SR.GetString("CircularOwner"));
                    }
                }
            }
            if ((form1 != null) && (form1.ParentInternal != null))
            {
                Control.CheckParentingCycle(form1.ParentInternal, toFind);
            }
        }

        private void ChildGotFocus(Control child)
        {
            if (this.IsActiveX)
            {
                this.ActiveXOnFocus(true);
            }
            if (this.parent != null)
            {
                this.parent.ChildGotFocus(child);
            }
        }

        public bool Contains(Control ctl)
        {
            while (ctl != null)
            {
                ctl = ctl.ParentInternal;
                if (ctl == null)
                {
                    return false;
                }
                if (ctl == this)
                {
                    return true;
                }
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual AccessibleObject CreateAccessibilityInstance()
        {
            return new ControlAccessibleObject(this);
        }

        public void CreateControl()
        {
            bool flag1 = this.Created;
            this.CreateControl(false);
            if (((this.Properties.GetObject(Control.PropBindingManager) == null) && (this.ParentInternal != null)) && !flag1)
            {
                this.OnBindingContextChanged(EventArgs.Empty);
            }
        }

        internal void CreateControl(bool fIgnoreVisible)
        {
            bool flag1 = (this.state & 1) == 0;
            flag1 = flag1 && this.Visible;
            if (flag1 || fIgnoreVisible)
            {
                this.state |= 1;
                bool flag2 = false;
                try
                {
                    if (!this.IsHandleCreated)
                    {
                        this.CreateHandle();
                    }
                    System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                    if (collection1 != null)
                    {
                        Control[] controlArray1 = new Control[collection1.Count];
                        collection1.CopyTo(controlArray1, 0);
                        Control[] controlArray2 = controlArray1;
                        for (int num1 = 0; num1 < controlArray2.Length; num1++)
                        {
                            Control control1 = controlArray2[num1];
                            if (control1.IsHandleCreated)
                            {
                                control1.SetParentHandle(this.Handle);
                            }
                            control1.CreateControl(fIgnoreVisible);
                        }
                    }
                    flag2 = true;
                }
                finally
                {
                    if (!flag2)
                    {
                        this.state &= -2;
                    }
                }
                this.OnCreateControl();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual System.Windows.Forms.Control.ControlCollection CreateControlsInstance()
        {
            return new System.Windows.Forms.Control.ControlCollection(this);
        }

        public Graphics CreateGraphics()
        {
            Graphics graphics1;
            using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
            {
                System.Windows.Forms.IntSecurity.CreateGraphicsForControl.Demand();
                graphics1 = this.CreateGraphicsInternal();
            }
            return graphics1;
        }

        internal Graphics CreateGraphicsInternal()
        {
            return Graphics.FromHwndInternal(this.Handle);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows)]
        protected virtual void CreateHandle()
        {
            IntPtr ptr1 = IntPtr.Zero;
            if (this.GetState(0x800))
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
            this.SetState(0x40000, true);
            if (Application.UseVisualStyles)
            {
                ptr1 = System.Windows.Forms.UnsafeNativeMethods.ThemingScope.Activate();
            }
            Rectangle rectangle1 = this.Bounds;
            try
            {
                System.Windows.Forms.CreateParams params1 = this.CreateParams;
                this.SetState(0x40000000, (params1.ExStyle & 0x400000) != 0);
                if (this.parent != null)
                {
                    Rectangle rectangle2 = this.parent.ClientRectangle;
                    if (!rectangle2.IsEmpty)
                    {
                        if (params1.X != -2147483648)
                        {
                            params1.X -= rectangle2.X;
                        }
                        if (params1.Y != -2147483648)
                        {
                            params1.Y -= rectangle2.Y;
                        }
                    }
                }
                if ((params1.Parent == IntPtr.Zero) && ((params1.Style & 0x40000000) != 0))
                {
                    Application.ParkHandle(params1);
                }
                this.window.CreateHandle(params1);
                this.UpdateReflectParent(true);
            }
            finally
            {
                this.SetState(0x40000, false);
                System.Windows.Forms.UnsafeNativeMethods.ThemingScope.Deactivate(ptr1);
            }
            if (this.Bounds != rectangle1)
            {
                LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual void DefWndProc(ref Message m)
        {
            this.window.DefWndProc(ref m);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), UIPermission(SecurityAction.LinkDemand, Window=UIPermissionWindow.AllWindows), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual void DestroyHandle()
        {
            if (this.RecreatingHandle)
            {
                Queue queue1 = (Queue) this.Properties.GetObject(Control.PropThreadCallbackList);
                if (queue1 != null)
                {
                    lock (queue1)
                    {
                        if (Control.threadCallbackMessage != 0)
                        {
                            System.Windows.Forms.NativeMethods.MSG msg1 = new System.Windows.Forms.NativeMethods.MSG();
                            if (System.Windows.Forms.UnsafeNativeMethods.PeekMessage(out msg1, new HandleRef(this, this.Handle), Control.threadCallbackMessage, Control.threadCallbackMessage, 0))
                            {
                                this.SetState(0x8000, true);
                            }
                        }
                    }
                }
            }
            if (!this.RecreatingHandle)
            {
                Queue queue2 = (Queue) this.Properties.GetObject(Control.PropThreadCallbackList);
                if (queue2 != null)
                {
                    lock (queue2)
                    {
                        Exception exception1 = new ObjectDisposedException(base.GetType().Name);
                        while (queue2.Count > 0)
                        {
                            ThreadMethodEntry entry1 = (ThreadMethodEntry) queue2.Dequeue();
                            entry1.exception = exception1;
                            entry1.Complete();
                        }
                    }
                }
            }
            if ((0x40 & ((int) ((long) System.Windows.Forms.UnsafeNativeMethods.GetWindowLong(new HandleRef(this.window, this.InternalHandle), -20)))) != 0)
            {
                System.Windows.Forms.UnsafeNativeMethods.DefMDIChildProc(this.InternalHandle, 0x10, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                this.window.DestroyHandle();
            }
            this.trackMouseEvent = null;
        }

        private void DetachContextMenu(object sender, EventArgs e)
        {
            this.ContextMenu = null;
        }

        private void DetachContextMenuStrip(object sender, EventArgs e)
        {
            this.ContextMenuStrip = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.GetState(0x200000))
            {
                object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                if (obj1 != null)
                {
                    IntPtr ptr1 = (IntPtr) obj1;
                    if (ptr1 != IntPtr.Zero)
                    {
                        System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(this, ptr1));
                    }
                    this.Properties.SetObject(Control.PropBackBrush, null);
                }
            }
            this.UpdateReflectParent(false);
            if (disposing)
            {
                if (this.GetState(0x1000))
                {
                    return;
                }
                if (this.GetState(0x40000))
                {
                    object[] objArray1 = new object[1] { "Dispose" } ;
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("ClosingWhileCreatingHandle", objArray1));
                }
                this.SetState(0x1000, true);
                this.SuspendLayout();
                try
                {
                    this.DisposeAxControls();
                    System.Windows.Forms.ContextMenu menu1 = (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
                    if (menu1 != null)
                    {
                        menu1.Disposed -= new EventHandler(this.DetachContextMenu);
                    }
                    this.ResetBindings();
                    if (this.IsHandleCreated)
                    {
                        this.DestroyHandle();
                    }
                    if (this.parent != null)
                    {
                        this.parent.Controls.Remove(this);
                    }
                    System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                    if (collection1 != null)
                    {
                        for (int num1 = 0; num1 < collection1.Count; num1++)
                        {
                            Control control1 = collection1[num1];
                            control1.parent = null;
                            control1.Dispose();
                        }
                        this.Properties.SetObject(Control.PropControlsCollection, null);
                    }
                    base.Dispose(disposing);
                    return;
                }
                finally
                {
                    this.ResumeLayout(false);
                    this.SetState(0x1000, false);
                    this.SetState(0x800, true);
                }
            }
            if (this.window != null)
            {
                IntPtr ptr2 = this.window.Handle;
                if (ptr2 != IntPtr.Zero)
                {
                    this.window.ReleaseHandle();
                    System.Windows.Forms.UnsafeNativeMethods.PostMessage(new HandleRef(this, ptr2), 0x10, 0, 0);
                }
            }
            base.Dispose(disposing);
        }

        internal virtual void DisposeAxControls()
        {
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].DisposeAxControls();
                }
            }
        }

        private void DisposeFontHandle()
        {
            if (this.Properties.ContainsObject(Control.PropFontHandleWrapper))
            {
                FontHandleWrapper wrapper1 = this.Properties.GetObject(Control.PropFontHandleWrapper) as FontHandleWrapper;
                if (wrapper1 != null)
                {
                    wrapper1.Dispose();
                }
                this.Properties.SetObject(Control.PropFontHandleWrapper, null);
            }
        }

        [UIPermission(SecurityAction.Demand, Clipboard=UIPermissionClipboard.OwnClipboard)]
        public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
        {
            int[] numArray2 = new int[1];
            int[] numArray1 = numArray2;
            System.Windows.Forms.UnsafeNativeMethods.IOleDropSource source1 = new DropSource(this);
            System.Runtime.InteropServices.ComTypes.IDataObject obj1 = null;
            if (data is System.Runtime.InteropServices.ComTypes.IDataObject)
            {
                obj1 = (System.Runtime.InteropServices.ComTypes.IDataObject) data;
            }
            else
            {
                DataObject obj2 = null;
                if (data is System.Windows.Forms.IDataObject)
                {
                    obj2 = new DataObject((System.Windows.Forms.IDataObject) data);
                }
                else
                {
                    obj2 = new DataObject();
                    obj2.SetData(data);
                }
                obj1 = obj2;
            }
            try
            {
                System.Windows.Forms.SafeNativeMethods.DoDragDrop(obj1, source1, (int) allowedEffects, numArray1);
            }
            catch (Exception exception1)
            {
                if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                {
                    throw;
                }
            }
            catch
            {
            }
            return (DragDropEffects) numArray1[0];
        }

        [UIPermission(SecurityAction.Demand, Window=UIPermissionWindow.AllWindows)]
        public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            if (((targetBounds.Width <= 0) || (targetBounds.Height <= 0)) || ((targetBounds.X < 0) || (targetBounds.Y < 0)))
            {
                throw new ArgumentException("targetBounds");
            }
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            int num1 = Math.Min(this.Width, targetBounds.Width);
            int num2 = Math.Min(this.Height, targetBounds.Height);
            Bitmap bitmap1 = new Bitmap(num1, num2, bitmap.PixelFormat);
            using (Graphics graphics1 = Graphics.FromImage(bitmap1))
            {
                IntPtr ptr1 = graphics1.GetHdc();
                System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 0x317, ptr1, (IntPtr) 30);
                using (Graphics graphics2 = Graphics.FromImage(bitmap))
                {
                    IntPtr ptr2 = graphics2.GetHdc();
                    System.Windows.Forms.SafeNativeMethods.BitBlt(new HandleRef(graphics2, ptr2), targetBounds.X, targetBounds.Y, num1, num2, new HandleRef(graphics1, ptr1), 0, 0, 0xcc0020);
                    graphics2.ReleaseHdcInternal(ptr2);
                }
                graphics1.ReleaseHdcInternal(ptr1);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public object EndInvoke(IAsyncResult asyncResult)
        {
            object obj1;
            using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
            {
                if (asyncResult == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                if (!(asyncResult is ThreadMethodEntry))
                {
                    throw new ArgumentException(System.Windows.Forms.SR.GetString("ControlBadAsyncResult"), "asyncResult");
                }
                ThreadMethodEntry entry1 = (ThreadMethodEntry) asyncResult;
                if (!asyncResult.IsCompleted)
                {
                    int num1;
                    Control control1 = this.FindMarshalingControl();
                    if (System.Windows.Forms.SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(control1, control1.Handle), out num1) == System.Windows.Forms.SafeNativeMethods.GetCurrentThreadId())
                    {
                        control1.InvokeMarshaledCallbacks();
                    }
                    else
                    {
                        asyncResult.AsyncWaitHandle.WaitOne();
                    }
                }
                if (entry1.exception != null)
                {
                    throw entry1.exception;
                }
                obj1 = entry1.retVal;
            }
            return obj1;
        }

        internal bool EndUpdateInternal()
        {
            return this.EndUpdateInternal(true);
        }

        internal bool EndUpdateInternal(bool invalidate)
        {
            if (this.updateCount <= 0)
            {
                return false;
            }
            this.updateCount = (short) (this.updateCount - 1);
            if (this.updateCount == 0)
            {
                this.SendMessage(11, -1, 0);
                if (invalidate)
                {
                    this.Invalidate();
                }
            }
            return true;
        }

        [UIPermission(SecurityAction.Demand, Window=UIPermissionWindow.AllWindows)]
        public Form FindForm()
        {
            return this.FindFormInternal();
        }

        internal Form FindFormInternal()
        {
            Control control1 = this;
            while ((control1 != null) && !(control1 is Form))
            {
                control1 = control1.ParentInternal;
            }
            return (Form) control1;
        }

        private Control FindMarshalingControl()
        {
            Control control3;
            lock (this)
            {
                Control control1 = this;
                while ((control1 != null) && !control1.IsHandleCreated)
                {
                    control1 = control1.ParentInternal;
                }
                if (control1 == null)
                {
                    control1 = this;
                }
                control3 = control1;
            }
            return control3;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool Focus()
        {
            System.Windows.Forms.IntSecurity.ModifyFocus.Demand();
            return this.FocusInternal();
        }

        internal virtual bool FocusInternal()
        {
            if (this.CanFocus)
            {
                System.Windows.Forms.UnsafeNativeMethods.SetFocus(new HandleRef(this, this.Handle));
            }
            if (this.Focused && (this.ParentInternal != null))
            {
                IContainerControl control1 = this.ParentInternal.GetContainerControlInternal();
                if (control1 != null)
                {
                    if (control1 is ContainerControl)
                    {
                        ((ContainerControl) control1).SetActiveControlInternal(this);
                    }
                    else
                    {
                        control1.ActiveControl = this;
                    }
                }
            }
            return this.Focused;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static Control FromChildHandle(IntPtr handle)
        {
            System.Windows.Forms.IntSecurity.ControlFromHandleOrLocation.Demand();
            return Control.FromChildHandleInternal(handle);
        }

        internal static Control FromChildHandleInternal(IntPtr handle)
        {
            while (handle != IntPtr.Zero)
            {
                Control control1 = Control.FromHandleInternal(handle);
                if (control1 != null)
                {
                    return control1;
                }
                handle = System.Windows.Forms.UnsafeNativeMethods.GetAncestor(new HandleRef(null, handle), 1);
            }
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static Control FromHandle(IntPtr handle)
        {
            System.Windows.Forms.IntSecurity.ControlFromHandleOrLocation.Demand();
            return Control.FromHandleInternal(handle);
        }

        internal static Control FromHandleInternal(IntPtr handle)
        {
            NativeWindow window1 = NativeWindow.FromHandle(handle);
            while ((window1 != null) && !(window1 is ControlNativeWindow))
            {
                window1 = window1.PreviousWindow;
            }
            if (window1 is ControlNativeWindow)
            {
                return ((ControlNativeWindow) window1).GetControl();
            }
            return null;
        }

        private AccessibleObject GetAccessibilityObject(int accObjId)
        {
            int num1 = accObjId;
            if (num1 != -4)
            {
                if (num1 == 0)
                {
                    return this.NcAccessibilityObject;
                }
            }
            else
            {
                return this.AccessibilityObject;
            }
            if (accObjId > 0)
            {
                return this.GetAccessibilityObjectById(accObjId);
            }
            return null;
        }

        protected virtual AccessibleObject GetAccessibilityObjectById(int objectId)
        {
            return null;
        }

        internal bool GetAnyDisposingInHierarchy()
        {
            Control control1 = this;
            bool flag1 = false;
            while (control1 != null)
            {
                if (control1.Disposing)
                {
                    flag1 = true;
                    break;
                }
                control1 = control1.parent;
            }
            return flag1;
        }

        protected AutoSizeMode GetAutoSizeMode()
        {
            return CommonProperties.GetAutoSizeMode(this);
        }

        internal static AutoValidate GetAutoValidateForControl(Control control)
        {
            ContainerControl control1 = control.ParentContainerControl;
            if (control1 == null)
            {
                return AutoValidate.EnablePreventFocusChange;
            }
            return control1.AutoValidate;
        }

        public Control GetChildAtPoint(Point pt)
        {
            return this.GetChildAtPoint(pt, GetChildAtPointSkip.None);
        }

        public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
        {
            int num1 = (int) skipValue;
            if ((num1 < 0) || (num1 > 7))
            {
                throw new InvalidEnumArgumentException("skipValue", num1, typeof(GetChildAtPointSkip));
            }
            IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ChildWindowFromPointEx(new HandleRef(null, this.Handle), pt.X, pt.Y, num1);
            Control control1 = Control.FromChildHandleInternal(ptr1);
            if ((control1 != null) && !this.IsDescendant(control1))
            {
                System.Windows.Forms.IntSecurity.ControlFromHandleOrLocation.Demand();
            }
            if (control1 != this)
            {
                return control1;
            }
            return null;
        }

        internal Control[] GetChildControlsInTabOrder(bool handleCreatedOnly)
        {
            ArrayList list1 = this.GetChildControlsTabOrderList(handleCreatedOnly);
            Control[] controlArray1 = new Control[list1.Count];
            for (int num1 = 0; num1 < list1.Count; num1++)
            {
                controlArray1[num1] = ((ControlTabOrderHolder) list1[num1]).control;
            }
            return controlArray1;
        }

        private ArrayList GetChildControlsTabOrderList(bool handleCreatedOnly)
        {
            ArrayList list1 = new ArrayList();
            foreach (Control control1 in this.Controls)
            {
                if (handleCreatedOnly && !control1.IsHandleCreated)
                {
                    continue;
                }
                list1.Add(new ControlTabOrderHolder(list1.Count, control1.TabIndex, control1));
            }
            list1.Sort(new ControlTabOrderComparer());
            return list1;
        }

        private static ArrayList GetChildWindows(IntPtr hWndParent)
        {
            ArrayList list1 = new ArrayList();
            for (IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(null, hWndParent), 5); ptr1 != IntPtr.Zero; ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(null, ptr1), 2))
            {
                list1.Add(ptr1);
            }
            return list1;
        }

        private int[] GetChildWindowsInTabOrder()
        {
            ArrayList list1 = this.GetChildWindowsTabOrderList();
            int[] numArray1 = new int[list1.Count];
            for (int num1 = 0; num1 < list1.Count; num1++)
            {
                numArray1[num1] = ((ControlTabOrderHolder) list1[num1]).oldOrder;
            }
            return numArray1;
        }

        private ArrayList GetChildWindowsTabOrderList()
        {
            ArrayList list1 = new ArrayList();
            ArrayList list2 = Control.GetChildWindows(this.Handle);
            foreach (IntPtr ptr1 in list2)
            {
                Control control1 = Control.FromHandleInternal(ptr1);
                int num1 = (control1 == null) ? -1 : control1.TabIndex;
                list1.Add(new ControlTabOrderHolder(list1.Count, num1, control1));
            }
            list1.Sort(new ControlTabOrderComparer());
            return list1;
        }

        public IContainerControl GetContainerControl()
        {
            System.Windows.Forms.IntSecurity.GetParent.Demand();
            return this.GetContainerControlInternal();
        }

        internal IContainerControl GetContainerControlInternal()
        {
            Control control1 = this;
            if ((control1 != null) && this.IsContainerControl)
            {
                control1 = control1.ParentInternal;
            }
            while ((control1 != null) && !Control.IsFocusManagingContainerControl(control1))
            {
                control1 = control1.ParentInternal;
            }
            return (IContainerControl) control1;
        }

        private static FontHandleWrapper GetDefaultFontHandleWrapper()
        {
            if (Control.defaultFontHandleWrapper == null)
            {
                Control.defaultFontHandleWrapper = new FontHandleWrapper(Control.DefaultFont);
            }
            return Control.defaultFontHandleWrapper;
        }

        internal virtual Control GetFirstChildControlInTabOrder(bool forward)
        {
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            Control control1 = null;
            if (collection1 != null)
            {
                if (forward)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        if ((control1 == null) || (control1.tabIndex > collection1[num1].tabIndex))
                        {
                            control1 = collection1[num1];
                        }
                    }
                    return control1;
                }
                for (int num2 = collection1.Count - 1; num2 >= 0; num2--)
                {
                    if ((control1 == null) || (control1.tabIndex < collection1[num2].tabIndex))
                    {
                        control1 = collection1[num2];
                    }
                }
            }
            return control1;
        }

        internal IntPtr GetHRgn(System.Drawing.Region region)
        {
            Graphics graphics1 = this.CreateGraphicsInternal();
            IntPtr ptr1 = region.GetHrgn(graphics1);
            System.Internal.HandleCollector.Add(ptr1, System.Windows.Forms.NativeMethods.CommonHandles.GDI);
            graphics1.Dispose();
            return ptr1;
        }

        private MenuItem GetMenuItemFromHandleId(IntPtr hmenu, int item)
        {
            MenuItem item1 = null;
            int num1 = System.Windows.Forms.UnsafeNativeMethods.GetMenuItemID(new HandleRef(null, hmenu), item);
            if (num1 == -1)
            {
                IntPtr ptr1 = IntPtr.Zero;
                ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetSubMenu(new HandleRef(null, hmenu), item);
                int num2 = System.Windows.Forms.UnsafeNativeMethods.GetMenuItemCount(new HandleRef(null, ptr1));
                MenuItem item2 = null;
                for (int num3 = 0; num3 < num2; num3++)
                {
                    item2 = this.GetMenuItemFromHandleId(ptr1, num3);
                    if (item2 != null)
                    {
                        Menu menu1 = item2.Parent;
                        if ((menu1 != null) && (menu1 is MenuItem))
                        {
                            item2 = (MenuItem) menu1;
                            break;
                        }
                        item2 = null;
                    }
                }
                return item2;
            }
            Command command1 = Command.GetCommandFromID(num1);
            if (command1 != null)
            {
                object obj1 = command1.Target;
                if ((obj1 != null) && (obj1 is MenuItem.MenuItemData))
                {
                    item1 = ((MenuItem.MenuItemData) obj1).baseItem;
                }
            }
            return item1;
        }

        public Control GetNextControl(Control ctl, bool forward)
        {
            if (!this.Contains(ctl))
            {
                ctl = this;
            }
            if (forward)
            {
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) ctl.Properties.GetObject(Control.PropControlsCollection);
                if (((collection1 != null) && (collection1.Count > 0)) && ((ctl == this) || !Control.IsFocusManagingContainerControl(ctl)))
                {
                    Control control1 = ctl.GetFirstChildControlInTabOrder(true);
                    if (control1 != null)
                    {
                        return control1;
                    }
                }
                while (ctl != this)
                {
                    int num1 = ctl.tabIndex;
                    bool flag1 = false;
                    Control control2 = null;
                    Control control3 = ctl.parent;
                    int num2 = 0;
                    System.Windows.Forms.Control.ControlCollection collection2 = (System.Windows.Forms.Control.ControlCollection) control3.Properties.GetObject(Control.PropControlsCollection);
                    if (collection2 != null)
                    {
                        num2 = collection2.Count;
                    }
                    for (int num3 = 0; num3 < num2; num3++)
                    {
                        if (collection2[num3] != ctl)
                        {
                            if (((collection2[num3].tabIndex >= num1) && ((control2 == null) || (control2.tabIndex > collection2[num3].tabIndex))) && ((collection2[num3].tabIndex != num1) || flag1))
                            {
                                control2 = collection2[num3];
                            }
                        }
                        else
                        {
                            flag1 = true;
                        }
                    }
                    if (control2 != null)
                    {
                        return control2;
                    }
                    ctl = ctl.parent;
                }
            }
            else
            {
                if (ctl != this)
                {
                    int num4 = ctl.tabIndex;
                    bool flag2 = false;
                    Control control4 = null;
                    Control control5 = ctl.parent;
                    int num5 = 0;
                    System.Windows.Forms.Control.ControlCollection collection3 = (System.Windows.Forms.Control.ControlCollection) control5.Properties.GetObject(Control.PropControlsCollection);
                    if (collection3 != null)
                    {
                        num5 = collection3.Count;
                    }
                    for (int num6 = num5 - 1; num6 >= 0; num6--)
                    {
                        if (collection3[num6] != ctl)
                        {
                            if (((collection3[num6].tabIndex <= num4) && ((control4 == null) || (control4.tabIndex < collection3[num6].tabIndex))) && ((collection3[num6].tabIndex != num4) || flag2))
                            {
                                control4 = collection3[num6];
                            }
                        }
                        else
                        {
                            flag2 = true;
                        }
                    }
                    if (control4 != null)
                    {
                        ctl = control4;
                    }
                    else
                    {
                        if (control5 == this)
                        {
                            return null;
                        }
                        return control5;
                    }
                }
                for (System.Windows.Forms.Control.ControlCollection collection4 = (System.Windows.Forms.Control.ControlCollection) ctl.Properties.GetObject(Control.PropControlsCollection); ((collection4 != null) && (collection4.Count > 0)) && ((ctl == this) || !Control.IsFocusManagingContainerControl(ctl)); collection4 = (System.Windows.Forms.Control.ControlCollection) ctl.Properties.GetObject(Control.PropControlsCollection))
                {
                    Control control6 = ctl.GetFirstChildControlInTabOrder(false);
                    if (control6 == null)
                    {
                        break;
                    }
                    ctl = control6;
                }
            }
            if (ctl != this)
            {
                return ctl;
            }
            return null;
        }

        private System.Drawing.Font GetParentFont()
        {
            if ((this.ParentInternal != null) && this.ParentInternal.CanAccessProperties)
            {
                return this.ParentInternal.Font;
            }
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
        {
            System.Drawing.Size size1;
            if (this.GetState(0x1800))
            {
                return CommonProperties.xGetPreferredSizeCache(this);
            }
            proposedSize = LayoutUtils.ConvertZeroToUnbounded(proposedSize);
            proposedSize = this.ApplySizeConstraints(proposedSize);
            System.Drawing.Size size2 = CommonProperties.xGetPreferredSizeCache(this);
            if (!size2.IsEmpty && ((proposedSize == LayoutUtils.MaxSize) || ((proposedSize.Width >= size2.Width) && (proposedSize.Height >= size2.Height))))
            {
                return size2;
            }
            this.CacheTextInternal = true;
            try
            {
                size1 = this.GetPreferredSizeCore(proposedSize);
            }
            finally
            {
                this.CacheTextInternal = false;
            }
            size1 = this.ApplySizeConstraints(size1);
            if (proposedSize == LayoutUtils.MaxSize)
            {
                CommonProperties.xSetPreferredSizeCache(this, size1);
            }
            return size1;
        }

        internal virtual System.Drawing.Size GetPreferredSizeCore(System.Drawing.Size proposedSize)
        {
            Rectangle rectangle1 = CommonProperties.GetSpecifiedBounds(this);
            return rectangle1.Size;
        }

        internal static IntPtr GetSafeHandle(IWin32Window window)
        {
            IntPtr ptr1 = IntPtr.Zero;
            Control control1 = window as Control;
            if (control1 != null)
            {
                return control1.Handle;
            }
            System.Windows.Forms.IntSecurity.AllWindows.Demand();
            ptr1 = window.Handle;
            if ((ptr1 != IntPtr.Zero) && !System.Windows.Forms.UnsafeNativeMethods.IsWindow(new HandleRef(null, ptr1)))
            {
                throw new Win32Exception(6);
            }
            return ptr1;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT(0, 0, 0, 0);
            System.Windows.Forms.CreateParams params1 = this.CreateParams;
            System.Windows.Forms.SafeNativeMethods.AdjustWindowRectEx(ref rect1, params1.Style, this.HasMenu, params1.ExStyle);
            float single1 = factor.Width;
            float single2 = factor.Height;
            int num1 = bounds.X;
            int num2 = bounds.Y;
            bool flag1 = !this.GetState(0x80000);
            if (flag1)
            {
                ISite site1 = this.Site;
                if ((site1 != null) && site1.DesignMode)
                {
                    IDesignerHost host1 = site1.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if ((host1 != null) && (host1.RootComponent == this))
                    {
                        flag1 = false;
                    }
                }
            }
            if (flag1)
            {
                if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
                {
                    num1 = (int) Math.Round((double) (bounds.X * single1));
                }
                if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
                {
                    num2 = (int) Math.Round((double) (bounds.Y * single2));
                }
            }
            int num3 = bounds.Width;
            int num4 = bounds.Height;
            if (((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth) && ((specified & BoundsSpecified.Width) != BoundsSpecified.None))
            {
                int num5 = rect1.right - rect1.left;
                int num6 = bounds.Width - num5;
                num3 = ((int) Math.Round((double) (num6 * single1))) + num5;
            }
            if (((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight) && ((specified & BoundsSpecified.Height) != BoundsSpecified.None))
            {
                int num7 = rect1.bottom - rect1.top;
                int num8 = bounds.Height - num7;
                num4 = ((int) Math.Round((double) (num8 * single2))) + num7;
            }
            return new Rectangle(num1, num2, num3, num4);
        }

        internal bool GetState(int flag)
        {
            return ((this.state & flag) != 0);
        }

        private bool GetState2(int flag)
        {
            return ((this.state2 & flag) != 0);
        }

        protected bool GetStyle(ControlStyles flag)
        {
            return ((this.controlStyle & flag) == flag);
        }

        protected bool GetTopLevel()
        {
            return ((this.state & 0x80000) != 0);
        }

        internal virtual bool GetVisibleCore()
        {
            if (!this.GetState(2))
            {
                return false;
            }
            if (this.ParentInternal == null)
            {
                return true;
            }
            return this.ParentInternal.GetVisibleCore();
        }

        private System.Windows.Forms.MouseButtons GetXButton(int wparam)
        {
            switch (wparam)
            {
                case 1:
                {
                    return System.Windows.Forms.MouseButtons.XButton1;
                }
                case 2:
                {
                    return System.Windows.Forms.MouseButtons.XButton2;
                }
            }
            return System.Windows.Forms.MouseButtons.None;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        private void HookMouseEvent()
        {
            if (!this.GetState(0x4000))
            {
                this.SetState(0x4000, true);
                if (this.trackMouseEvent == null)
                {
                    this.trackMouseEvent = new System.Windows.Forms.NativeMethods.TRACKMOUSEEVENT();
                    this.trackMouseEvent.dwFlags = 3;
                    this.trackMouseEvent.hwndTrack = this.Handle;
                }
                System.Windows.Forms.SafeNativeMethods.TrackMouseEvent(this.trackMouseEvent);
            }
        }

        internal void ImeModeRestricted(bool restricted)
        {
            this.ImeModeRestricted(restricted, false);
        }

        internal void ImeModeRestricted(bool restricted, bool forceUnrestrictedImeMode)
        {
            if (!base.DesignMode)
            {
                if (restricted)
                {
                    this.SetUnrestrictedImeMode(this.CachedImeMode, forceUnrestrictedImeMode);
                    this.CurrentImeContextMode = ImeModeConversion.InputLanguageImeModeDisabled;
                }
                else
                {
                    System.Windows.Forms.ImeMode mode1 = this.UnrestrictedImeMode;
                    if (mode1 != System.Windows.Forms.ImeMode.NoControl)
                    {
                        this.CurrentImeContextMode = mode1;
                    }
                    else if (this.IsHandleCreated)
                    {
                        ImeContext.Enable(this.Handle);
                    }
                }
            }
        }

        internal virtual IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
        {
            if (!this.GetStyle(ControlStyles.UserPaint))
            {
                System.Windows.Forms.SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.ForeColor));
                System.Windows.Forms.SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.BackColor));
                return this.BackColorBrush;
            }
            return System.Windows.Forms.UnsafeNativeMethods.GetStockObject(5);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void InitLayout()
        {
            this.LayoutEngine.InitLayout(this, BoundsSpecified.All);
        }

        private void InitMouseWheelSupport()
        {
            if (!Control.mouseWheelInit)
            {
                Control.mouseWheelRoutingNeeded = !SystemInformation.NativeMouseWheelSupport;
                if (Control.mouseWheelRoutingNeeded)
                {
                    IntPtr ptr1 = IntPtr.Zero;
                    ptr1 = System.Windows.Forms.UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
                    if (ptr1 != IntPtr.Zero)
                    {
                        int num1 = System.Windows.Forms.SafeNativeMethods.RegisterWindowMessage("MSWHEEL_ROLLMSG");
                        if (num1 != 0)
                        {
                            Control.mouseWheelMessage = num1;
                        }
                    }
                }
                Control.mouseWheelInit = true;
            }
        }

        private void InitScaling(BoundsSpecified specified)
        {
            this.requiredScaling = (byte) (this.requiredScaling | ((byte) (specified & BoundsSpecified.All)));
        }

        public void Invalidate()
        {
            this.Invalidate(false);
        }

        public void Invalidate(bool invalidateChildren)
        {
            if (this.IsHandleCreated)
            {
                if (invalidateChildren)
                {
                    System.Windows.Forms.SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), (System.Windows.Forms.NativeMethods.COMRECT) null, System.Windows.Forms.NativeMethods.NullHandleRef, 0x85);
                }
                else
                {
                    using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
                    {
                        System.Windows.Forms.SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), (System.Windows.Forms.NativeMethods.COMRECT) null, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
                    }
                }
                this.NotifyInvalidate(this.ClientRectangle);
            }
        }

        public void Invalidate(Rectangle rc)
        {
            this.Invalidate(rc, false);
        }

        public void Invalidate(System.Drawing.Region region)
        {
            this.Invalidate(region, false);
        }

        public void Invalidate(Rectangle rc, bool invalidateChildren)
        {
            if (rc.IsEmpty)
            {
                this.Invalidate(invalidateChildren);
            }
            else if (this.IsHandleCreated)
            {
                if (invalidateChildren)
                {
                    System.Windows.Forms.NativeMethods.RECT rect1 = System.Windows.Forms.NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
                    System.Windows.Forms.SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), ref rect1, System.Windows.Forms.NativeMethods.NullHandleRef, 0x85);
                }
                else
                {
                    System.Windows.Forms.NativeMethods.RECT rect2 = System.Windows.Forms.NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
                    using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
                    {
                        System.Windows.Forms.SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), ref rect2, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
                    }
                }
                this.NotifyInvalidate(rc);
            }
        }

        public void Invalidate(System.Drawing.Region region, bool invalidateChildren)
        {
            if (region == null)
            {
                this.Invalidate(invalidateChildren);
            }
            else if (this.IsHandleCreated)
            {
                IntPtr ptr1 = this.GetHRgn(region);
                try
                {
                    if (invalidateChildren)
                    {
                        System.Windows.Forms.SafeNativeMethods.RedrawWindow(new HandleRef(this, this.Handle), (System.Windows.Forms.NativeMethods.COMRECT) null, new HandleRef(region, ptr1), 0x85);
                    }
                    else
                    {
                        using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
                        {
                            System.Windows.Forms.SafeNativeMethods.InvalidateRgn(new HandleRef(this, this.Handle), new HandleRef(region, ptr1), !this.GetStyle(ControlStyles.Opaque));
                        }
                    }
                }
                finally
                {
                    System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(region, ptr1));
                }
                Rectangle rectangle1 = Rectangle.Empty;
                using (Graphics graphics1 = this.CreateGraphicsInternal())
                {
                    rectangle1 = Rectangle.Ceiling(region.GetBounds(graphics1));
                }
                this.OnInvalidated(new InvalidateEventArgs(rectangle1));
            }
        }

        public object Invoke(Delegate method)
        {
            return this.Invoke(method, null);
        }

        public object Invoke(Delegate method, params object[] args)
        {
            object obj1;
            using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
            {
                Control control1 = this.FindMarshalingControl();
                obj1 = control1.MarshaledInvoke(this, method, args, true);
            }
            return obj1;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void InvokeGotFocus(Control toInvoke, EventArgs e)
        {
            if (toInvoke != null)
            {
                toInvoke.OnGotFocus(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void InvokeLostFocus(Control toInvoke, EventArgs e)
        {
            if (toInvoke != null)
            {
                toInvoke.OnLostFocus(e);
            }
        }

        private void InvokeMarshaledCallback(ThreadMethodEntry tme)
        {
            if (tme.executionContext != null)
            {
                if (Control.invokeMarshaledCallbackHelperDelegate == null)
                {
                    Control.invokeMarshaledCallbackHelperDelegate = new ContextCallback(Control.InvokeMarshaledCallbackHelper);
                }
                tme.syncContext = SynchronizationContext.Current;
                ExecutionContext.Run(tme.executionContext, Control.invokeMarshaledCallbackHelperDelegate, tme);
            }
            else
            {
                Control.InvokeMarshaledCallbackHelper(tme);
            }
        }

        private static void InvokeMarshaledCallbackDo(ThreadMethodEntry tme)
        {
            if (tme.method is EventHandler)
            {
                if ((tme.args == null) || (tme.args.Length < 1))
                {
                    ((EventHandler) tme.method)(tme.caller, EventArgs.Empty);
                }
                else if (tme.args.Length < 2)
                {
                    ((EventHandler) tme.method)(tme.args[0], EventArgs.Empty);
                }
                else
                {
                    ((EventHandler) tme.method)(tme.args[0], (EventArgs) tme.args[1]);
                }
            }
            else if (tme.method is MethodInvoker)
            {
                ((MethodInvoker) tme.method)();
            }
            else if (tme.method is WaitCallback)
            {
                ((WaitCallback) tme.method)(tme.args[0]);
            }
            else
            {
                tme.retVal = tme.method.DynamicInvoke(tme.args);
            }
        }

        private static void InvokeMarshaledCallbackHelper(object obj)
        {
            ThreadMethodEntry entry1 = (ThreadMethodEntry) obj;
            if (entry1.syncContext != null)
            {
                SynchronizationContext context1 = SynchronizationContext.Current;
                try
                {
                    SynchronizationContext.SetSynchronizationContext(entry1.syncContext);
                    Control.InvokeMarshaledCallbackDo(entry1);
                    return;
                }
                finally
                {
                    SynchronizationContext.SetSynchronizationContext(context1);
                }
            }
            Control.InvokeMarshaledCallbackDo(entry1);
        }

        private void InvokeMarshaledCallbacks()
        {
            ThreadMethodEntry entry1 = null;
            Queue queue1 = (Queue) this.Properties.GetObject(Control.PropThreadCallbackList);
            lock (queue1)
            {
                if (queue1.Count > 0)
                {
                    entry1 = (ThreadMethodEntry) queue1.Dequeue();
                }
                goto Label_00E8;
            }
        Label_0041:
            if (entry1.method != null)
            {
                try
                {
                    if (NativeWindow.WndProcShouldBeDebuggable && !entry1.synchronous)
                    {
                        this.InvokeMarshaledCallback(entry1);
                    }
                    else
                    {
                        try
                        {
                            this.InvokeMarshaledCallback(entry1);
                        }
                        catch (Exception exception1)
                        {
                            entry1.exception = exception1.GetBaseException();
                        }
                        catch
                        {
                            entry1.exception = new ApplicationException(System.Windows.Forms.SR.GetString("NonExceptionWasThrown"));
                        }
                    }
                }
                finally
                {
                    entry1.Complete();
                    if ((!NativeWindow.WndProcShouldBeDebuggable && (entry1.exception != null)) && !entry1.synchronous)
                    {
                        Application.OnThreadException(entry1.exception);
                    }
                }
            }
            lock (queue1)
            {
                if (queue1.Count > 0)
                {
                    entry1 = (ThreadMethodEntry) queue1.Dequeue();
                }
                else
                {
                    entry1 = null;
                }
            }
        Label_00E8:
            if (entry1 == null)
            {
                return;
            }
            goto Label_0041;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void InvokeOnClick(Control toInvoke, EventArgs e)
        {
            if (toInvoke != null)
            {
                toInvoke.OnClick(e);
            }
        }

        protected void InvokePaint(Control c, PaintEventArgs e)
        {
            c.OnPaint(e);
        }

        protected void InvokePaintBackground(Control c, PaintEventArgs e)
        {
            c.OnPaintBackground(e);
        }

        internal bool IsDescendant(Control descendant)
        {
            for (Control control1 = descendant; control1 != null; control1 = control1.ParentInternal)
            {
                if (control1 == this)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsFocusManagingContainerControl(Control ctl)
        {
            if ((ctl.controlStyle & ControlStyles.ContainerControl) == ControlStyles.ContainerControl)
            {
                return (ctl is IContainerControl);
            }
            return false;
        }

        internal bool IsFontSet()
        {
            System.Drawing.Font font1 = (System.Drawing.Font) this.Properties.GetObject(Control.PropFont);
            if (font1 != null)
            {
                return true;
            }
            return false;
        }

        [UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows)]
        protected virtual bool IsInputChar(char charCode)
        {
            int num1 = 0;
            if (charCode == '\t')
            {
                num1 = 0x86;
            }
            else
            {
                num1 = 0x84;
            }
            return ((((int) this.SendMessage(0x87, 0, 0)) & num1) != 0);
        }

        [UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows)]
        protected virtual bool IsInputKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) == Keys.Alt)
            {
                return false;
            }
            int num1 = 4;
            Keys keys1 = keyData & Keys.KeyCode;
            if (keys1 != Keys.Tab)
            {
                switch (keys1)
                {
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                    {
                        num1 = 5;
                        goto Label_0040;
                    }
                }
            }
            else
            {
                num1 = 6;
            }
        Label_0040:
            if (this.IsHandleCreated)
            {
                return ((((int) this.SendMessage(0x87, 0, 0)) & num1) != 0);
            }
            return false;
        }

        public static bool IsKeyLocked(Keys keyVal)
        {
            if (((keyVal == Keys.Insert) || (keyVal == Keys.NumLock)) || ((keyVal == Keys.Capital) || (keyVal == Keys.Scroll)))
            {
                int num1 = System.Windows.Forms.UnsafeNativeMethods.GetKeyState((int) keyVal);
                if ((keyVal != Keys.Insert) && (keyVal != Keys.Capital))
                {
                    return ((num1 & 0x8001) != 0);
                }
                return ((num1 & 1) != 0);
            }
            throw new NotSupportedException(System.Windows.Forms.SR.GetString("ControlIsKeyLockedNumCapsScrollLockKeysSupportedOnly"));
        }

        public static bool IsMnemonic(char charCode, string text)
        {
            if (charCode == '&')
            {
                return false;
            }
            if (text != null)
            {
                char ch2;
                int num1 = -1;
                char ch1 = char.ToUpper(charCode, CultureInfo.CurrentCulture);
                do
                {
                    if ((num1 + 1) >= text.Length)
                    {
                        goto Label_006E;
                    }
                    num1 = text.IndexOf('&', (int) (num1 + 1)) + 1;
                    if ((num1 <= 0) || (num1 >= text.Length))
                    {
                        goto Label_006E;
                    }
                    ch2 = char.ToUpper(text[num1], CultureInfo.CurrentCulture);
                }
                while ((ch2 != ch1) && (char.ToLower(ch2, CultureInfo.CurrentCulture) != char.ToLower(ch1, CultureInfo.CurrentCulture)));
                return true;
            }
        Label_006E:
            return false;
        }

        internal bool IsUpdating()
        {
            return (this.updateCount > 0);
        }

        private bool IsValidBackColor(System.Drawing.Color c)
        {
            if ((!c.IsEmpty && !this.GetStyle(ControlStyles.SupportsTransparentBackColor)) && (c.A < 0xff))
            {
                return false;
            }
            return true;
        }

        private void ListenToUserPreferenceChanged(bool listen)
        {
            if (this.GetState2(4))
            {
                if (listen)
                {
                    return;
                }
                this.SetState2(4, false);
                SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
            }
            else if (listen)
            {
                this.SetState2(4, true);
                SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
            }
        }

        private object MarshaledInvoke(Control caller, Delegate method, object[] args, bool synchronous)
        {
            int num1;
            Queue queue1;
            if (!this.IsHandleCreated)
            {
                throw new InvalidOperationException(System.Windows.Forms.SR.GetString("ErrorNoMarshalingThread"));
            }
            ActiveXImpl impl1 = (ActiveXImpl) this.Properties.GetObject(Control.PropActiveXImpl);
            if (impl1 != null)
            {
                System.Windows.Forms.IntSecurity.UnmanagedCode.Demand();
            }
            bool flag1 = false;
            if ((System.Windows.Forms.SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num1) == System.Windows.Forms.SafeNativeMethods.GetCurrentThreadId()) && synchronous)
            {
                flag1 = true;
            }
            ExecutionContext context1 = null;
            if (!flag1)
            {
                context1 = ExecutionContext.Capture();
            }
            ThreadMethodEntry entry1 = new ThreadMethodEntry(caller, method, args, synchronous, context1);
            lock (this)
            {
                queue1 = (Queue) this.Properties.GetObject(Control.PropThreadCallbackList);
                if (queue1 == null)
                {
                    queue1 = new Queue();
                    this.Properties.SetObject(Control.PropThreadCallbackList, queue1);
                }
            }
            lock (queue1)
            {
                if (Control.threadCallbackMessage == 0)
                {
                    Control.threadCallbackMessage = System.Windows.Forms.SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_ThreadCallbackMessage");
                }
                queue1.Enqueue(entry1);
            }
            if (flag1)
            {
                this.InvokeMarshaledCallbacks();
            }
            else
            {
                System.Windows.Forms.UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
            }
            if (!synchronous)
            {
                return entry1;
            }
            if (!entry1.IsCompleted)
            {
                entry1.AsyncWaitHandle.WaitOne();
            }
            if (entry1.exception != null)
            {
                throw entry1.exception;
            }
            return entry1.retVal;
        }

        private void MarshalStringToMessage(string value, ref Message m)
        {
            if (m.LParam == IntPtr.Zero)
            {
                m.Result = (IntPtr) ((value.Length + 1) * Marshal.SystemDefaultCharSize);
            }
            else if (((int) ((long) m.WParam)) < (value.Length + 1))
            {
                m.Result = (IntPtr) (-1);
            }
            else
            {
                byte[] buffer1;
                byte[] buffer2;
                char[] chArray2 = new char[1];
                char[] chArray1 = chArray2;
                if (Marshal.SystemDefaultCharSize == 1)
                {
                    buffer2 = Encoding.Default.GetBytes(value);
                    buffer1 = Encoding.Default.GetBytes(chArray1);
                }
                else
                {
                    buffer2 = Encoding.Unicode.GetBytes(value);
                    buffer1 = Encoding.Unicode.GetBytes(chArray1);
                }
                Marshal.Copy(buffer2, 0, m.LParam, buffer2.Length);
                Marshal.Copy(buffer1, 0, (IntPtr) (((long) m.LParam) + buffer2.Length), buffer1.Length);
                m.Result = (IntPtr) ((buffer2.Length + buffer1.Length) / Marshal.SystemDefaultCharSize);
            }
        }

        internal void NotifyEnter()
        {
            this.OnEnter(EventArgs.Empty);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
        {
            this.OnInvalidated(new InvalidateEventArgs(invalidatedArea));
        }

        internal void NotifyLeave()
        {
            this.OnLeave(EventArgs.Empty);
        }

        private void NotifyValidated()
        {
            this.OnValidated(EventArgs.Empty);
        }

        private bool NotifyValidating()
        {
            CancelEventArgs args1 = new CancelEventArgs();
            this.OnValidating(args1);
            return args1.Cancel;
        }

        internal virtual void NotifyValidationResult(object sender, CancelEventArgs ev)
        {
            this.ValidationCancelled = ev.Cancel;
        }

        protected virtual void OnAutoSizeChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventAutoSizeChanged] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBackColorChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                if (obj1 != null)
                {
                    if (this.GetState(0x200000))
                    {
                        IntPtr ptr1 = (IntPtr) obj1;
                        if (ptr1 != IntPtr.Zero)
                        {
                            System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(this, ptr1));
                        }
                    }
                    this.Properties.SetObject(Control.PropBackBrush, null);
                }
                this.Invalidate();
                EventHandler handler1 = base.Events[Control.EventBackColor] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].OnParentBackColorChanged(e);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBackgroundImageChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                this.Invalidate();
                EventHandler handler1 = base.Events[Control.EventBackgroundImage] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].OnParentBackgroundImageChanged(e);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                this.Invalidate();
                EventHandler handler1 = base.Events[Control.EventBackgroundImageLayout] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnBindingContextChanged(EventArgs e)
        {
            if (this.Properties.GetObject(Control.PropBindings) != null)
            {
                this.UpdateBindings();
            }
            EventHandler handler1 = base.Events[Control.EventBindingContext] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].OnParentBindingContextChanged(e);
                }
            }
        }

        internal virtual void OnBoundsUpdate(int x, int y, int width, int height)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCausesValidationChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventCausesValidation] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnChangeUICues(UICuesEventArgs e)
        {
            UICuesEventHandler handler1 = (UICuesEventHandler) base.Events[Control.EventChangeUICues];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        internal virtual void OnChildLayoutResuming(Control child, bool performLayout)
        {
            if (this.ParentInternal != null)
            {
                this.ParentInternal.OnChildLayoutResuming(child, performLayout);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnClick(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventClick];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnClientSizeChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventClientSize] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnContextMenuChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventContextMenu] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnContextMenuStripChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventContextMenuStrip] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnControlAdded(ControlEventArgs e)
        {
            ControlEventHandler handler1 = (ControlEventHandler) base.Events[Control.EventControlAdded];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnControlRemoved(ControlEventArgs e)
        {
            ControlEventHandler handler1 = (ControlEventHandler) base.Events[Control.EventControlRemoved];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCreateControl()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCursorChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventCursor] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].OnParentCursorChanged(e);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDockChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventDock] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDoubleClick(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventDoubleClick];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDragDrop(DragEventArgs drgevent)
        {
            DragEventHandler handler1 = (DragEventHandler) base.Events[Control.EventDragDrop];
            if (handler1 != null)
            {
                handler1(this, drgevent);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDragEnter(DragEventArgs drgevent)
        {
            DragEventHandler handler1 = (DragEventHandler) base.Events[Control.EventDragEnter];
            if (handler1 != null)
            {
                handler1(this, drgevent);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDragLeave(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventDragLeave];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDragOver(DragEventArgs drgevent)
        {
            DragEventHandler handler1 = (DragEventHandler) base.Events[Control.EventDragOver];
            if (handler1 != null)
            {
                handler1(this, drgevent);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnEnabledChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                if (this.IsHandleCreated)
                {
                    System.Windows.Forms.SafeNativeMethods.EnableWindow(new HandleRef(this, this.Handle), this.Enabled);
                    if (this.GetStyle(ControlStyles.UserPaint))
                    {
                        this.Invalidate();
                        this.Update();
                    }
                }
                EventHandler handler1 = base.Events[Control.EventEnabled] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].OnParentEnabledChanged(e);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnEnter(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventEnter];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnFontChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                this.Invalidate();
                if (this.Properties.ContainsInteger(Control.PropFontHeight))
                {
                    this.Properties.SetInteger(Control.PropFontHeight, -1);
                }
                this.DisposeFontHandle();
                if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
                {
                    this.SetWindowFont();
                }
                EventHandler handler1 = base.Events[Control.EventFont] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                using (LayoutTransaction transaction1 = new LayoutTransaction(this, this, PropertyNames.Font, false))
                {
                    if (collection1 != null)
                    {
                        for (int num1 = 0; num1 < collection1.Count; num1++)
                        {
                            collection1[num1].OnParentFontChanged(e);
                        }
                    }
                }
                LayoutTransaction.DoLayout(this, this, PropertyNames.Font);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnForeColorChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                this.Invalidate();
                EventHandler handler1 = base.Events[Control.EventForeColor] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].OnParentForeColorChanged(e);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            GiveFeedbackEventHandler handler1 = (GiveFeedbackEventHandler) base.Events[Control.EventGiveFeedback];
            if (handler1 != null)
            {
                handler1(this, gfbevent);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnGotFocus(EventArgs e)
        {
            if (this.IsActiveX)
            {
                this.ActiveXOnFocus(true);
            }
            if (this.parent != null)
            {
                this.parent.ChildGotFocus(this);
            }
            EventHandler handler1 = (EventHandler) base.Events[Control.EventGotFocus];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnHandleCreated(EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (!this.GetStyle(ControlStyles.UserPaint))
                {
                    this.SetWindowFont();
                }
                this.SetAcceptDrops(this.AllowDrop);
                System.Drawing.Region region1 = (System.Drawing.Region) this.Properties.GetObject(Control.PropRegion);
                if (region1 != null)
                {
                    IntPtr ptr1 = this.GetHRgn(region1);
                    try
                    {
                        if (this.IsActiveX)
                        {
                            ptr1 = this.ActiveXMergeRegion(ptr1);
                        }
                        if (System.Windows.Forms.UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, ptr1), System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
                        {
                            ptr1 = IntPtr.Zero;
                        }
                    }
                    finally
                    {
                        if (ptr1 != IntPtr.Zero)
                        {
                            System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(null, ptr1));
                        }
                    }
                }
                ControlAccessibleObject obj1 = this.Properties.GetObject(Control.PropAccessibility) as ControlAccessibleObject;
                ControlAccessibleObject obj2 = this.Properties.GetObject(Control.PropNcAccessibility) as ControlAccessibleObject;
                IntPtr ptr2 = this.Handle;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    if (obj1 != null)
                    {
                        obj1.Handle = ptr2;
                    }
                    if (obj2 != null)
                    {
                        obj2.Handle = ptr2;
                    }
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                if ((this.text != null) && (this.text.Length != 0))
                {
                    System.Windows.Forms.UnsafeNativeMethods.SetWindowText(new HandleRef(this, this.Handle), this.text);
                }
                if ((!(this is ScrollableControl) && !this.IsMirrored) && (this.GetState2(2) && !this.GetState2(1)))
                {
                    this.BeginInvoke(new EventHandler(this.OnSetScrollPosition));
                    this.SetState2(1, true);
                    this.SetState2(2, false);
                }
                if (this.GetState2(8))
                {
                    this.ListenToUserPreferenceChanged(this.GetTopLevel());
                }
            }
            EventHandler handler1 = (EventHandler) base.Events[Control.EventHandleCreated];
            if (handler1 != null)
            {
                handler1(this, e);
            }
            if (this.IsHandleCreated && this.GetState(0x8000))
            {
                System.Windows.Forms.UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
                this.SetState(0x8000, false);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnHandleDestroyed(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventHandleDestroyed];
            if (handler1 != null)
            {
                handler1(this, e);
            }
            this.UpdateReflectParent(false);
            if (!this.RecreatingHandle)
            {
                if (this.GetState(0x200000))
                {
                    object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                    if (obj1 != null)
                    {
                        IntPtr ptr1 = (IntPtr) obj1;
                        if (ptr1 != IntPtr.Zero)
                        {
                            System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(this, ptr1));
                        }
                        this.Properties.SetObject(Control.PropBackBrush, null);
                    }
                }
                this.ListenToUserPreferenceChanged(false);
            }
            try
            {
                if (!this.GetAnyDisposingInHierarchy())
                {
                    this.text = this.Text;
                    if ((this.text != null) && (this.text.Length == 0))
                    {
                        this.text = null;
                    }
                }
                this.SetAcceptDrops(false);
            }
            catch (Exception exception1)
            {
                if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                {
                    throw;
                }
            }
            catch
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnHelpRequested(HelpEventArgs hevent)
        {
            HelpEventHandler handler1 = (HelpEventHandler) base.Events[Control.EventHelpRequested];
            if (handler1 != null)
            {
                handler1(this, hevent);
                hevent.Handled = true;
            }
            if (!hevent.Handled && (this.ParentInternal != null))
            {
                this.ParentInternal.OnHelpRequested(hevent);
            }
        }

        protected virtual void OnImeModeChanged(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventImeModeChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnInvalidated(InvalidateEventArgs e)
        {
            if (this.IsActiveX)
            {
                this.ActiveXViewChanged();
            }
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].OnParentInvalidated(e);
                }
            }
            InvalidateEventHandler handler1 = (InvalidateEventHandler) base.Events[Control.EventInvalidated];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyEventHandler handler1 = (KeyEventHandler) base.Events[Control.EventKeyDown];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            KeyPressEventHandler handler1 = (KeyPressEventHandler) base.Events[Control.EventKeyPress];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyEventHandler handler1 = (KeyEventHandler) base.Events[Control.EventKeyUp];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLayout(LayoutEventArgs levent)
        {
            if (this.IsActiveX)
            {
                this.ActiveXViewChanged();
            }
            LayoutEventHandler handler1 = (LayoutEventHandler) base.Events[Control.EventLayout];
            if (handler1 != null)
            {
                handler1(this, levent);
            }
            bool flag1 = this.LayoutEngine.Layout(this, levent);
            if (flag1 && (this.ParentInternal != null))
            {
                this.ParentInternal.SetState(0x800000, true);
            }
        }

        internal virtual void OnLayoutResuming(bool performLayout)
        {
            if (this.ParentInternal != null)
            {
                this.ParentInternal.OnChildLayoutResuming(this, performLayout);
            }
        }

        internal virtual void OnLayoutSuspended()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLeave(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventLeave];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLocationChanged(EventArgs e)
        {
            this.OnMove(EventArgs.Empty);
            EventHandler handler1 = base.Events[Control.EventLocation] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLostFocus(EventArgs e)
        {
            if (this.IsActiveX)
            {
                this.ActiveXOnFocus(false);
            }
            EventHandler handler1 = (EventHandler) base.Events[Control.EventLostFocus];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        protected virtual void OnMarginChanged(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMarginChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseCaptureChanged(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMouseCaptureChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseClick];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseDoubleClick];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseDown];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseEnter(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMouseEnter];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseHover(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMouseHover];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseLeave(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMouseLeave];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseMove];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseUp];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[Control.EventMouseWheel];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMove(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventMove];
            if (handler1 != null)
            {
                handler1(this, e);
            }
            if (this.RenderTransparent)
            {
                this.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnNotifyMessage(Message m)
        {
        }

        protected virtual void OnPaddingChanged(EventArgs e)
        {
            if (this.GetStyle(ControlStyles.ResizeRedraw))
            {
                this.Invalidate();
            }
            EventHandler handler1 = (EventHandler) base.Events[Control.EventPaddingChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPaint(PaintEventArgs e)
        {
            PaintEventHandler handler1 = (PaintEventHandler) base.Events[Control.EventPaint];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPaintBackground(PaintEventArgs pevent)
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
            System.Windows.Forms.UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), out rect1);
            this.PaintBackground(pevent, new Rectangle(rect1.left, rect1.top, rect1.right, rect1.bottom));
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentBackColorChanged(EventArgs e)
        {
            System.Drawing.Color color1 = this.Properties.GetColor(Control.PropBackColor);
            if (color1.IsEmpty)
            {
                this.OnBackColorChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentBackgroundImageChanged(EventArgs e)
        {
            this.OnBackgroundImageChanged(e);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentBindingContextChanged(EventArgs e)
        {
            if (this.Properties.GetObject(Control.PropBindingManager) == null)
            {
                this.OnBindingContextChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventParent] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
            if (this.TopMostParent.IsActiveX)
            {
                this.OnTopMostActiveXParentChanged(EventArgs.Empty);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentCursorChanged(EventArgs e)
        {
            if (this.Properties.GetObject(Control.PropCursor) == null)
            {
                this.OnCursorChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentEnabledChanged(EventArgs e)
        {
            if (this.GetState(4))
            {
                this.OnEnabledChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentFontChanged(EventArgs e)
        {
            if (this.Properties.GetObject(Control.PropFont) == null)
            {
                this.OnFontChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentForeColorChanged(EventArgs e)
        {
            System.Drawing.Color color1 = this.Properties.GetColor(Control.PropForeColor);
            if (color1.IsEmpty)
            {
                this.OnForeColorChanged(e);
            }
        }

        internal virtual void OnParentHandleRecreated()
        {
            Control control1 = this.ParentInternal;
            if ((control1 != null) && this.IsHandleCreated)
            {
                System.Windows.Forms.UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), new HandleRef(control1, control1.Handle));
                this.UpdateZOrder();
            }
            this.SetState(0x20000000, false);
            if (this.ReflectParent == this.ParentInternal)
            {
                this.RecreateHandle();
            }
        }

        internal virtual void OnParentHandleRecreating()
        {
            this.SetState(0x20000000, true);
            if (this.IsHandleCreated)
            {
                Application.ParkHandle(new HandleRef(this, this.Handle));
            }
        }

        private void OnParentInvalidated(InvalidateEventArgs e)
        {
            if (this.RenderTransparent && this.IsHandleCreated)
            {
                Rectangle rectangle1 = e.InvalidRect;
                Point point1 = this.Location;
                rectangle1.Offset(-point1.X, -point1.Y);
                rectangle1 = Rectangle.Intersect(this.ClientRectangle, rectangle1);
                if (!rectangle1.IsEmpty)
                {
                    this.Invalidate(rectangle1);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentRightToLeftChanged(EventArgs e)
        {
            if (!this.Properties.ContainsInteger(Control.PropRightToLeft) || (this.Properties.GetInteger(Control.PropRightToLeft) == 2))
            {
                this.OnRightToLeftChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnParentVisibleChanged(EventArgs e)
        {
            if (this.GetState(2))
            {
                this.OnVisibleChanged(e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            PreviewKeyDownEventHandler handler1 = (PreviewKeyDownEventHandler) base.Events[Control.EventPreviewKeyDown];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPrint(PaintEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (this.GetStyle(ControlStyles.UserPaint))
            {
                this.PaintWithErrorHandling(e, 1, false);
                e.ResetGraphics();
                this.PaintWithErrorHandling(e, 2, false);
            }
            else
            {
                Message message1;
                PrintPaintEventArgs args1 = e as PrintPaintEventArgs;
                bool flag1 = false;
                IntPtr ptr1 = IntPtr.Zero;
                if (args1 == null)
                {
                    IntPtr ptr2 = (IntPtr) 30;
                    ptr1 = e.HDC;
                    if (ptr1 == IntPtr.Zero)
                    {
                        ptr1 = e.Graphics.GetHdc(true);
                        flag1 = true;
                    }
                    message1 = Message.Create(this.Handle, 0x318, ptr1, ptr2);
                }
                else
                {
                    message1 = args1.Message;
                }
                try
                {
                    this.DefWndProc(ref message1);
                }
                finally
                {
                    if (flag1)
                    {
                        e.Graphics.ReleaseHdcInternal(ptr1);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            QueryContinueDragEventHandler handler1 = (QueryContinueDragEventHandler) base.Events[Control.EventQueryContinueDrag];
            if (handler1 != null)
            {
                handler1(this, qcdevent);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnRegionChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventRegionChanged] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnResize(EventArgs e)
        {
            if (((this.controlStyle & ControlStyles.ResizeRedraw) == ControlStyles.ResizeRedraw) || this.GetState(0x400000))
            {
                this.Invalidate();
            }
            LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
            EventHandler handler1 = (EventHandler) base.Events[Control.EventResize];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnRightToLeftChanged(EventArgs e)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                this.SetState2(2, true);
                this.RecreateHandle();
                EventHandler handler1 = base.Events[Control.EventRightToLeft] as EventHandler;
                if (handler1 != null)
                {
                    handler1(this, e);
                }
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].OnParentRightToLeftChanged(e);
                    }
                }
            }
        }

        private void OnSetScrollPosition(object sender, EventArgs e)
        {
            this.SetState2(1, false);
            if (!(this is ScrollableControl) && !this.IsMirrored)
            {
                System.Windows.Forms.NativeMethods.SCROLLINFO scrollinfo1 = new System.Windows.Forms.NativeMethods.SCROLLINFO();
                scrollinfo1.cbSize = Marshal.SizeOf(typeof(System.Windows.Forms.NativeMethods.SCROLLINFO));
                scrollinfo1.fMask = 1;
                if (System.Windows.Forms.UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, this.Handle), 0, scrollinfo1))
                {
                    scrollinfo1.nPos = (this.RightToLeft == System.Windows.Forms.RightToLeft.Yes) ? scrollinfo1.nMax : scrollinfo1.nMin;
                    this.SendMessage(0x114, System.Windows.Forms.NativeMethods.Util.MAKELPARAM(4, scrollinfo1.nPos), 0);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnSizeChanged(EventArgs e)
        {
            this.OnResize(EventArgs.Empty);
            EventHandler handler1 = base.Events[Control.EventSize] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnStyleChanged(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventStyleChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnSystemColorsChanged(EventArgs e)
        {
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].OnSystemColorsChanged(EventArgs.Empty);
                }
            }
            this.Invalidate();
            EventHandler handler1 = (EventHandler) base.Events[Control.EventSystemColorsChanged];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnTabIndexChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventTabIndex] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnTabStopChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventTabStop] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnTextChanged(EventArgs e)
        {
            EventHandler handler1 = base.Events[Control.EventText] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        internal virtual void OnTopMostActiveXParentChanged(EventArgs e)
        {
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    collection1[num1].OnTopMostActiveXParentChanged(e);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnValidated(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventValidated];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnValidating(CancelEventArgs e)
        {
            CancelEventHandler handler1 = (CancelEventHandler) base.Events[Control.EventValidating];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                this.UnhookMouseEvent();
                this.trackMouseEvent = null;
            }
            if ((((this.parent != null) && this.Visible) && !this.Created) && !this.GetAnyDisposingInHierarchy())
            {
                this.CreateControl();
            }
            EventHandler handler1 = base.Events[Control.EventVisible] as EventHandler;
            if (handler1 != null)
            {
                handler1(this, e);
            }
            System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
            if (collection1 != null)
            {
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    Control control1 = collection1[num1];
                    if (control1.Visible)
                    {
                        control1.OnParentVisibleChanged(e);
                    }
                }
            }
        }

        private static void PaintBackColor(PaintEventArgs e, Rectangle rectangle, System.Drawing.Color backColor)
        {
            if (backColor.A > 0)
            {
                System.Drawing.Color color1 = e.Graphics.GetNearestColor(backColor);
                using (Brush brush1 = new SolidBrush(color1))
                {
                    e.Graphics.FillRectangle(brush1, rectangle);
                }
            }
        }

        internal void PaintBackground(PaintEventArgs e, Rectangle rectangle)
        {
            this.PaintBackground(e, rectangle, this.BackColor, Point.Empty);
        }

        internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, System.Drawing.Color backColor)
        {
            this.PaintBackground(e, rectangle, backColor, Point.Empty);
        }

        internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, System.Drawing.Color backColor, Point scrollOffset)
        {
            if (this.RenderColorTransparent(backColor))
            {
                this.PaintTransparentBackground(e, rectangle);
            }
            bool flag1 = ((this is Form) || (this is MdiClient)) && this.IsMirrored;
            if (((this.BackgroundImage != null) && !DisplayInformation.HighContrast) && !flag1)
            {
                if ((this.BackgroundImageLayout == ImageLayout.Tile) && ControlPaint.IsImageTransparent(this.BackgroundImage))
                {
                    this.PaintTransparentBackground(e, rectangle);
                }
                Point point1 = scrollOffset;
                ScrollableControl control1 = this as ScrollableControl;
                if ((control1 != null) && (point1 != Point.Empty))
                {
                    point1 = ((ScrollableControl) this).AutoScrollPosition;
                }
                if (ControlPaint.IsImageTransparent(this.BackgroundImage))
                {
                    Control.PaintBackColor(e, rectangle, backColor);
                }
                ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, this.BackColor, this.BackgroundImageLayout, this.ClientRectangle, rectangle, point1, this.RightToLeft);
            }
            else
            {
                Control.PaintBackColor(e, rectangle, backColor);
            }
        }

        private void PaintException(PaintEventArgs e)
        {
            int num1 = 2;
            using (Pen pen1 = new Pen(System.Drawing.Color.Red, (float) num1))
            {
                Rectangle rectangle1 = this.ClientRectangle;
                Rectangle rectangle2 = rectangle1;
                rectangle2.X++;
                rectangle2.Y++;
                rectangle2.Width--;
                rectangle2.Height--;
                e.Graphics.DrawRectangle(pen1, rectangle2.X, rectangle2.Y, (int) (rectangle2.Width - 1), (int) (rectangle2.Height - 1));
                rectangle2.Inflate(-1, -1);
                e.Graphics.FillRectangle(Brushes.White, rectangle2);
                e.Graphics.DrawLine(pen1, rectangle1.Left, rectangle1.Top, rectangle1.Right, rectangle1.Bottom);
                e.Graphics.DrawLine(pen1, rectangle1.Left, rectangle1.Bottom, rectangle1.Right, rectangle1.Top);
            }
        }

        internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
        {
            this.PaintTransparentBackground(e, rectangle, null);
        }

        internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle, System.Drawing.Region transparentRegion)
        {
            Graphics graphics1 = e.Graphics;
            Control control1 = this.ParentInternal;
            if (control1 != null)
            {
                if (Application.RenderWithVisualStyles && control1.RenderTransparencyWithVisualStyles)
                {
                    GraphicsState state1 = null;
                    if (transparentRegion != null)
                    {
                        state1 = graphics1.Save();
                    }
                    try
                    {
                        if (transparentRegion != null)
                        {
                            graphics1.Clip = transparentRegion;
                        }
                        ButtonRenderer.DrawParentBackground(graphics1, rectangle, this);
                        return;
                    }
                    finally
                    {
                        if (state1 != null)
                        {
                            graphics1.Restore(state1);
                        }
                    }
                }
                Rectangle rectangle1 = new Rectangle(-this.Left, -this.Top, control1.Width, control1.Height);
                Rectangle rectangle2 = new Rectangle(rectangle.Left + this.Left, rectangle.Top + this.Top, rectangle.Width, rectangle.Height);
                HandleRef ref1 = new HandleRef(this, graphics1.GetHdc());
                try
                {
                    WindowsFormsUtils.DCMapping mapping1 = new WindowsFormsUtils.DCMapping(ref1, rectangle1);
                    try
                    {
                        using (PaintEventArgs args1 = new PaintEventArgs(mapping1.Graphics, rectangle2))
                        {
                            if (transparentRegion != null)
                            {
                                args1.Graphics.Clip = transparentRegion;
                                args1.Graphics.TranslateClip(-rectangle1.X, -rectangle1.Y);
                            }
                            try
                            {
                                this.InvokePaintBackground(control1, args1);
                                this.InvokePaint(control1, args1);
                                return;
                            }
                            finally
                            {
                                if (transparentRegion != null)
                                {
                                    args1.Graphics.TranslateClip(rectangle1.X, rectangle1.Y);
                                }
                            }
                            return;
                        }
                    }
                    finally
                    {
                        mapping1.Dispose();
                    }
                    return;
                }
                finally
                {
                    graphics1.ReleaseHdcInternal(ref1.Handle);
                }
            }
            graphics1.FillRectangle(SystemBrushes.Control, rectangle);
        }

        private void PaintWithErrorHandling(PaintEventArgs e, short layer, bool disposeEventArgs)
        {
            try
            {
                this.CacheTextInternal = true;
                if (this.GetState(0x400000))
                {
                    if (layer == 1)
                    {
                        this.PaintException(e);
                    }
                }
                else
                {
                    bool flag1 = true;
                    try
                    {
                        switch (layer)
                        {
                            case 1:
                            {
                                break;
                            }
                            case 2:
                            {
                                this.OnPaint(e);
                                goto Label_0050;
                            }
                            default:
                            {
                                goto Label_0050;
                            }
                        }
                        if (!this.GetStyle(ControlStyles.Opaque))
                        {
                            this.OnPaintBackground(e);
                        }
                    Label_0050:
                        flag1 = false;
                    }
                    finally
                    {
                        if (flag1)
                        {
                            this.SetState(0x400000, true);
                            this.Invalidate();
                        }
                    }
                }
            }
            finally
            {
                this.CacheTextInternal = false;
                if (disposeEventArgs)
                {
                    e.Dispose();
                }
            }
        }

        internal bool PerformContainerValidation(ValidationConstraints validationConstraints)
        {
            bool flag1 = false;
            foreach (Control control1 in this.Controls)
            {
                if ((((validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren) && control1.GetStyle(ControlStyles.ContainerControl)) && control1.PerformContainerValidation(validationConstraints))
                {
                    flag1 = true;
                }
                if ((((validationConstraints & ValidationConstraints.Selectable) != ValidationConstraints.Selectable) || control1.GetStyle(ControlStyles.Selectable)) && ((((validationConstraints & ValidationConstraints.Enabled) != ValidationConstraints.Enabled) || control1.Enabled) && ((((validationConstraints & ValidationConstraints.Visible) != ValidationConstraints.Visible) || control1.Visible) && ((((validationConstraints & ValidationConstraints.TabStop) != ValidationConstraints.TabStop) || control1.TabStop) && control1.PerformControlValidation(true)))))
                {
                    flag1 = true;
                }
            }
            return flag1;
        }

        internal bool PerformControlValidation(bool bulkValidation)
        {
            if (this.CausesValidation)
            {
                if (this.NotifyValidating())
                {
                    return true;
                }
                if (bulkValidation || NativeWindow.WndProcShouldBeDebuggable)
                {
                    this.NotifyValidated();
                }
                else
                {
                    try
                    {
                        this.NotifyValidated();
                    }
                    catch (Exception exception1)
                    {
                        Application.OnThreadException(exception1);
                    }
                    catch
                    {
                        Application.OnThreadException(new ApplicationException(System.Windows.Forms.SR.GetString("NonExceptionWasThrown")));
                    }
                }
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void PerformLayout()
        {
            if (this.cachedLayoutEventArgs != null)
            {
                this.PerformLayout(this.cachedLayoutEventArgs);
                this.cachedLayoutEventArgs = null;
                this.SetState2(0x40, false);
            }
            else
            {
                this.PerformLayout(null, null);
            }
        }

        internal void PerformLayout(LayoutEventArgs args)
        {
            if (!this.GetAnyDisposingInHierarchy())
            {
                if (this.layoutSuspendCount > 0)
                {
                    this.SetState(0x200, true);
                    if ((this.cachedLayoutEventArgs == null) || (this.GetState2(0x40) && (args != null)))
                    {
                        this.cachedLayoutEventArgs = args;
                        if (this.GetState2(0x40))
                        {
                            this.SetState2(0x40, false);
                        }
                    }
                    else
                    {
                        this.LayoutEngine.ProcessSuspendedLayoutEventArgs(this, args);
                    }
                }
                else
                {
                    this.layoutSuspendCount = 1;
                    try
                    {
                        this.CacheTextInternal = true;
                        this.OnLayout(args);
                    }
                    finally
                    {
                        this.CacheTextInternal = false;
                        this.SetState(0x800200, false);
                        this.layoutSuspendCount = 0;
                        if ((this.ParentInternal != null) && this.ParentInternal.GetState(0x800000))
                        {
                            LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.PreferredSize);
                        }
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void PerformLayout(Control affectedControl, string affectedProperty)
        {
            this.PerformLayout(new LayoutEventArgs(affectedControl, affectedProperty));
        }

        public Point PointToClient(Point p)
        {
            return this.PointToClientInternal(p);
        }

        internal Point PointToClientInternal(Point p)
        {
            System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT(p.X, p.Y);
            System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(System.Windows.Forms.NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), point1, 1);
            return new Point(point1.x, point1.y);
        }

        public Point PointToScreen(Point p)
        {
            System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT(p.X, p.Y);
            System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, point1, 1);
            return new Point(point1.x, point1.y);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected PreProcessControlState PreProcessControlMessage(ref Message msg)
        {
            return Control.PreProcessControlMessageInternal(null, ref msg);
        }

        internal static PreProcessControlState PreProcessControlMessageInternal(Control target, ref Message msg)
        {
            PreProcessControlState state2;
            if (target == null)
            {
                target = Control.FromChildHandle(msg.HWnd);
            }
            if (target == null)
            {
                return PreProcessControlState.MessageNotNeeded;
            }
            target.SetState2(0x80, false);
            target.SetState2(0x100, false);
            target.SetState2(0x200, true);
            try
            {
                Keys keys1 = ((Keys) ((int) msg.WParam)) | Control.ModifierKeys;
                if ((msg.Msg == 0x100) || (msg.Msg == 260))
                {
                    target.ProcessUICues(ref msg);
                    PreviewKeyDownEventArgs args1 = new PreviewKeyDownEventArgs(keys1);
                    target.OnPreviewKeyDown(args1);
                    if (args1.IsInputKey)
                    {
                        return PreProcessControlState.MessageNeeded;
                    }
                }
                PreProcessControlState state1 = PreProcessControlState.MessageNotNeeded;
                if (!target.PreProcessMessage(ref msg))
                {
                    if ((msg.Msg == 0x100) || (msg.Msg == 260))
                    {
                        if (target.GetState2(0x80) || target.IsInputKey(keys1))
                        {
                            state1 = PreProcessControlState.MessageNeeded;
                        }
                    }
                    else if (((msg.Msg == 0x102) || (msg.Msg == 0x106)) && (target.GetState2(0x100) || target.IsInputChar((char) ((ushort) ((int) msg.WParam)))))
                    {
                        state1 = PreProcessControlState.MessageNeeded;
                    }
                }
                else
                {
                    state1 = PreProcessControlState.MessageProcessed;
                }
                state2 = state1;
            }
            finally
            {
                target.SetState2(0x200, false);
            }
            return state2;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        public virtual bool PreProcessMessage(ref Message msg)
        {
            if ((msg.Msg == 0x100) || (msg.Msg == 260))
            {
                if (!this.GetState2(0x200))
                {
                    this.ProcessUICues(ref msg);
                }
                Keys keys1 = ((Keys) ((int) msg.WParam)) | Control.ModifierKeys;
                if (this.ProcessCmdKey(ref msg, keys1))
                {
                    return true;
                }
                if (this.IsInputKey(keys1))
                {
                    this.SetState2(0x80, true);
                    return false;
                }
                System.Windows.Forms.IntSecurity.ModifyFocus.Assert();
                try
                {
                    return this.ProcessDialogKey(keys1);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }
            if ((msg.Msg == 0x102) || (msg.Msg == 0x106))
            {
                if ((msg.Msg == 0x102) && this.IsInputChar((char) ((ushort) ((int) msg.WParam))))
                {
                    this.SetState2(0x100, true);
                    return false;
                }
                return this.ProcessDialogChar((char) ((ushort) ((int) msg.WParam)));
            }
            return false;
        }

        private void PrintToMetaFile(HandleRef hDC, IntPtr lParam)
        {
            lParam = (IntPtr) (((long) lParam) & -17);
            System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT();
            System.Windows.Forms.SafeNativeMethods.GetViewportOrgEx(hDC, point1);
            HandleRef ref1 = new HandleRef(null, System.Windows.Forms.SafeNativeMethods.CreateRectRgn(point1.x, point1.y, point1.x + this.Width, point1.y + this.Height));
            try
            {
                System.Windows.Forms.SafeNativeMethods.SelectClipRgn(hDC, ref1);
                this.PrintToMetaFileRecursive(hDC, lParam, new Rectangle(Point.Empty, this.Size));
            }
            finally
            {
                System.Windows.Forms.SafeNativeMethods.DeleteObject(ref1);
            }
        }

        private void PrintToMetaFile_SendPrintMessage(HandleRef hDC, IntPtr lParam)
        {
            if (this.GetStyle(ControlStyles.UserPaint))
            {
                this.SendMessage(0x317, hDC.Handle, lParam);
            }
            else
            {
                if (this.Controls.Count == 0)
                {
                    lParam = (IntPtr) (((long) lParam) | 0x10);
                }
                using (MetafileDCWrapper wrapper1 = new MetafileDCWrapper(hDC, this.Size))
                {
                    this.SendMessage(0x317, wrapper1.HDC, lParam);
                }
            }
        }

        internal virtual void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
        {
            WindowsFormsUtils.DCMapping mapping1 = new WindowsFormsUtils.DCMapping(hDC, bounds);
            try
            {
                this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr) (((long) lParam) & -5));
                System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
                System.Windows.Forms.UnsafeNativeMethods.GetWindowRect(new HandleRef(null, this.Handle), out rect1);
                Point point1 = this.PointToScreen(Point.Empty);
                point1 = new Point(point1.X - rect1.left, point1.Y - rect1.top);
                Rectangle rectangle1 = new Rectangle(point1, this.ClientSize);
                WindowsFormsUtils.DCMapping mapping2 = new WindowsFormsUtils.DCMapping(hDC, rectangle1);
                try
                {
                    this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr) (((long) lParam) & -3));
                    for (int num2 = this.Controls.Count - 1; num2 >= 0; num2--)
                    {
                        Control control1 = this.Controls[num2];
                        if (control1.Visible)
                        {
                            control1.PrintToMetaFileRecursive(hDC, lParam, control1.Bounds);
                        }
                    }
                }
                finally
                {
                    mapping2.Dispose();
                }
            }
            finally
            {
                mapping1.Dispose();
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            System.Windows.Forms.ContextMenu menu1 = (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
            if ((menu1 != null) && menu1.ProcessCmdKey(ref msg, keyData, this))
            {
                return true;
            }
            if (this.parent != null)
            {
                return this.parent.ProcessCmdKey(ref msg, keyData);
            }
            return false;
        }

        [UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows), UIPermission(SecurityAction.LinkDemand, Window=UIPermissionWindow.AllWindows)]
        protected virtual bool ProcessDialogChar(char charCode)
        {
            if (this.parent != null)
            {
                return this.parent.ProcessDialogChar(charCode);
            }
            return false;
        }

        [UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows), UIPermission(SecurityAction.LinkDemand, Window=UIPermissionWindow.AllWindows)]
        protected virtual bool ProcessDialogKey(Keys keyData)
        {
            if (this.parent != null)
            {
                return this.parent.ProcessDialogKey(keyData);
            }
            return false;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual bool ProcessKeyEventArgs(ref Message m)
        {
            KeyEventArgs args1 = null;
            KeyPressEventArgs args2 = null;
            IntPtr ptr1 = IntPtr.Zero;
            if ((m.Msg == 0x102) || (m.Msg == 0x106))
            {
                int num1 = this.Properties.GetInteger(Control.PropCharsToIgnore);
                if (num1 > 0)
                {
                    num1--;
                    this.Properties.SetInteger(Control.PropCharsToIgnore, num1);
                    return false;
                }
                args2 = new KeyPressEventArgs((char) ((ushort) ((long) m.WParam)));
                this.OnKeyPress(args2);
                ptr1 = (IntPtr) args2.KeyChar;
            }
            else if (m.Msg == 0x286)
            {
                int num2 = this.Properties.GetInteger(Control.PropCharsToIgnore);
                if (Marshal.SystemDefaultCharSize == 1)
                {
                    char ch1 = '\0';
                    byte[] buffer3 = new byte[2] { (byte) (((int) ((long) m.WParam)) >> 8), (byte) ((long) m.WParam) } ;
                    byte[] buffer1 = buffer3;
                    char[] chArray1 = new char[1];
                    int num3 = System.Windows.Forms.UnsafeNativeMethods.MultiByteToWideChar(0, 1, buffer1, buffer1.Length, chArray1, 0);
                    if (num3 <= 0)
                    {
                        throw new Win32Exception();
                    }
                    chArray1 = new char[num3];
                    System.Windows.Forms.UnsafeNativeMethods.MultiByteToWideChar(0, 1, buffer1, buffer1.Length, chArray1, chArray1.Length);
                    if (chArray1[0] != '\0')
                    {
                        ch1 = chArray1[0];
                        num2 += 2;
                    }
                    else if ((chArray1[0] == '\0') && (chArray1.Length >= 2))
                    {
                        ch1 = chArray1[1];
                        num2++;
                    }
                    this.Properties.SetInteger(Control.PropCharsToIgnore, num2);
                    args2 = new KeyPressEventArgs(ch1);
                }
                else
                {
                    num2 += (3 - Marshal.SystemDefaultCharSize);
                    this.Properties.SetInteger(Control.PropCharsToIgnore, num2);
                    args2 = new KeyPressEventArgs((char) ((ushort) ((long) m.WParam)));
                }
                char ch2 = args2.KeyChar;
                this.OnKeyPress(args2);
                if (args2.KeyChar == ch2)
                {
                    ptr1 = m.WParam;
                }
                else if (Marshal.SystemDefaultCharSize == 1)
                {
                    char[] chArray2 = new char[1] { args2.KeyChar } ;
                    string text1 = new string(chArray2);
                    byte[] buffer2 = null;
                    int num4 = System.Windows.Forms.UnsafeNativeMethods.WideCharToMultiByte(0, 0, text1, text1.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
                    if (num4 >= 2)
                    {
                        buffer2 = new byte[num4];
                        System.Windows.Forms.UnsafeNativeMethods.WideCharToMultiByte(0, 0, text1, text1.Length, buffer2, buffer2.Length, IntPtr.Zero, IntPtr.Zero);
                        int num5 = Marshal.SizeOf(typeof(IntPtr));
                        if (num4 > num5)
                        {
                            num4 = num5;
                        }
                        long num6 = 0;
                        for (int num7 = 0; num7 < num4; num7++)
                        {
                            num6 = num6 << 8;
                            num6 |= buffer2[num7];
                        }
                        ptr1 = (IntPtr) num6;
                    }
                    else if (num4 == 1)
                    {
                        buffer2 = new byte[num4];
                        System.Windows.Forms.UnsafeNativeMethods.WideCharToMultiByte(0, 0, text1, text1.Length, buffer2, buffer2.Length, IntPtr.Zero, IntPtr.Zero);
                        ptr1 = (IntPtr) buffer2[0];
                    }
                    else
                    {
                        ptr1 = m.WParam;
                    }
                }
                else
                {
                    ptr1 = (IntPtr) args2.KeyChar;
                }
            }
            else
            {
                args1 = new KeyEventArgs(((Keys) ((int) ((long) m.WParam))) | Control.ModifierKeys);
                if ((m.Msg == 0x100) || (m.Msg == 260))
                {
                    this.OnKeyDown(args1);
                }
                else
                {
                    this.OnKeyUp(args1);
                }
            }
            if (args2 != null)
            {
                m.WParam = ptr1;
                return args2.Handled;
            }
            if (args1.SuppressKeyPress)
            {
                this.RemovePendingMessages(0x102, 0x102);
                this.RemovePendingMessages(0x106, 0x106);
                this.RemovePendingMessages(0x286, 0x286);
            }
            return args1.Handled;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected internal virtual bool ProcessKeyMessage(ref Message m)
        {
            if ((this.parent != null) && this.parent.ProcessKeyPreview(ref m))
            {
                return true;
            }
            return this.ProcessKeyEventArgs(ref m);
        }

        [SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual bool ProcessKeyPreview(ref Message m)
        {
            if (this.parent != null)
            {
                return this.parent.ProcessKeyPreview(ref m);
            }
            return false;
        }

        [UIPermission(SecurityAction.LinkDemand, Window=UIPermissionWindow.AllWindows), UIPermission(SecurityAction.InheritanceDemand, Window=UIPermissionWindow.AllWindows)]
        protected internal virtual bool ProcessMnemonic(char charCode)
        {
            return false;
        }

        internal void ProcessUICues(ref Message msg)
        {
            Keys keys1 = ((Keys) ((int) msg.WParam)) & Keys.KeyCode;
            int num1 = (int) this.SendMessage(0x129, 0, 0);
            int num2 = 0;
            if (((keys1 == Keys.F10) || (keys1 == Keys.Menu)) && ((num1 & 2) != 0))
            {
                num2 |= 2;
            }
            if ((keys1 == Keys.Tab) && ((num1 & 1) != 0))
            {
                num2 |= 1;
            }
            if (num2 != 0)
            {
                Control control1 = this.TopMostParent;
                System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(control1, control1.Handle), (System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(null, control1.Handle)) == IntPtr.Zero) ? 0x127 : 0x128, (IntPtr) (2 | (num2 << 0x10)), IntPtr.Zero);
            }
        }

        internal void RaiseCreateHandleEvent(EventArgs e)
        {
            EventHandler handler1 = (EventHandler) base.Events[Control.EventHandleCreated];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void RaiseDragEvent(object key, DragEventArgs e)
        {
            DragEventHandler handler1 = (DragEventHandler) base.Events[key];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void RaiseKeyEvent(object key, KeyEventArgs e)
        {
            KeyEventHandler handler1 = (KeyEventHandler) base.Events[key];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void RaiseMouseEvent(object key, MouseEventArgs e)
        {
            MouseEventHandler handler1 = (MouseEventHandler) base.Events[key];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void RaisePaintEvent(object key, PaintEventArgs e)
        {
            PaintEventHandler handler1 = (PaintEventHandler) base.Events[Control.EventPaint];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void RecreateHandle()
        {
            this.RecreateHandleCore();
        }

        internal virtual void RecreateHandleCore()
        {
            lock (this)
            {
                if (!this.IsHandleCreated)
                {
                    return;
                }
                bool flag1 = this.ContainsFocus;
                bool flag2 = (this.state & 1) != 0;
                if (this.GetState(0x4000))
                {
                    this.SetState(0x2000, true);
                    this.UnhookMouseEvent();
                }
                HandleRef ref1 = new HandleRef(this, System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle)));
                try
                {
                    Control[] controlArray1 = null;
                    this.state |= 0x10;
                    try
                    {
                        System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                        if ((collection1 != null) && (collection1.Count > 0))
                        {
                            controlArray1 = new Control[collection1.Count];
                            for (int num1 = 0; num1 < collection1.Count; num1++)
                            {
                                Control control1 = collection1[num1];
                                if ((control1 != null) && control1.IsHandleCreated)
                                {
                                    control1.OnParentHandleRecreating();
                                    controlArray1[num1] = control1;
                                }
                                else
                                {
                                    controlArray1[num1] = null;
                                }
                            }
                        }
                        this.DestroyHandle();
                        this.CreateHandle();
                    }
                    finally
                    {
                        this.state &= -17;
                        if (controlArray1 != null)
                        {
                            for (int num2 = 0; num2 < controlArray1.Length; num2++)
                            {
                                Control control2 = controlArray1[num2];
                                if ((control2 != null) && control2.IsHandleCreated)
                                {
                                    control2.OnParentHandleRecreated();
                                }
                            }
                        }
                    }
                    if (flag2)
                    {
                        this.CreateControl();
                    }
                }
                finally
                {
                    if (((ref1.Handle != IntPtr.Zero) && ((Control.FromHandleInternal(ref1.Handle) == null) || (this.parent == null))) && System.Windows.Forms.UnsafeNativeMethods.IsWindow(ref1))
                    {
                        System.Windows.Forms.UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), ref1);
                    }
                }
                if (flag1)
                {
                    this.FocusInternal();
                }
            }
        }

        public Rectangle RectangleToClient(Rectangle r)
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = System.Windows.Forms.NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
            System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(System.Windows.Forms.NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), out rect1, 2);
            return Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom);
        }

        public Rectangle RectangleToScreen(Rectangle r)
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = System.Windows.Forms.NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
            System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, out rect1, 2);
            return Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected static bool ReflectMessage(IntPtr hWnd, ref Message m)
        {
            System.Windows.Forms.IntSecurity.SendMessages.Demand();
            return Control.ReflectMessageInternal(hWnd, ref m);
        }

        internal static bool ReflectMessageInternal(IntPtr hWnd, ref Message m)
        {
            Control control1 = Control.FromHandleInternal(hWnd);
            if (control1 == null)
            {
                return false;
            }
            m.Result = control1.SendMessage((int) (0x2000 + m.Msg), m.WParam, m.LParam);
            return true;
        }

        public virtual void Refresh()
        {
            this.Invalidate(true);
            this.Update();
        }

        private void RemovePendingMessages(int msgMin, int msgMax)
        {
            if (!this.IsDisposed)
            {
                System.Windows.Forms.NativeMethods.MSG msg1 = new System.Windows.Forms.NativeMethods.MSG();
                IntPtr ptr1 = this.Handle;
                while (System.Windows.Forms.UnsafeNativeMethods.PeekMessage(out msg1, new HandleRef(this, ptr1), msgMin, msgMax, 1))
                {
                }
            }
        }

        internal virtual void RemoveReflectChild()
        {
        }

        private bool RenderColorTransparent(System.Drawing.Color c)
        {
            if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
            {
                return (c.A < 0xff);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ResetBackColor()
        {
            this.BackColor = System.Drawing.Color.Empty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBindings()
        {
            ControlBindingsCollection collection1 = (ControlBindingsCollection) this.Properties.GetObject(Control.PropBindings);
            if (collection1 != null)
            {
                collection1.Clear();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ResetCursor()
        {
            this.Cursor = null;
        }

        private void ResetEnabled()
        {
            this.Enabled = true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ResetFont()
        {
            this.Font = null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ResetForeColor()
        {
            this.ForeColor = System.Drawing.Color.Empty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetImeMode()
        {
            this.ImeMode = this.DefaultImeMode;
        }

        private void ResetLocation()
        {
            this.Location = new Point(0, 0);
        }

        private void ResetMargin()
        {
            this.Margin = this.DefaultMargin;
        }

        private void ResetMinimumSize()
        {
            this.MinimumSize = this.DefaultMinimumSize;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void ResetMouseEventArgs()
        {
            if (this.GetState(0x4000))
            {
                this.UnhookMouseEvent();
                this.HookMouseEvent();
            }
        }

        private void ResetPadding()
        {
            this.Padding = this.DefaultPadding;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ResetRightToLeft()
        {
            this.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
        }

        private void ResetSize()
        {
            this.Size = this.DefaultSize;
        }

        public virtual void ResetText()
        {
            this.Text = string.Empty;
        }

        private void ResetVisible()
        {
            this.Visible = true;
        }

        public void ResumeLayout()
        {
            this.ResumeLayout(true);
        }

        public void ResumeLayout(bool performLayout)
        {
            bool flag1 = false;
            if (this.layoutSuspendCount > 0)
            {
                if (this.layoutSuspendCount == 1)
                {
                    this.layoutSuspendCount = (byte) (this.layoutSuspendCount + 1);
                    try
                    {
                        this.OnLayoutResuming(performLayout);
                    }
                    finally
                    {
                        this.layoutSuspendCount = (byte) (this.layoutSuspendCount - 1);
                    }
                }
                this.layoutSuspendCount = (byte) (this.layoutSuspendCount - 1);
                if (((this.layoutSuspendCount == 0) && this.GetState(0x200)) && performLayout)
                {
                    this.PerformLayout();
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                this.SetState2(0x40, true);
            }
            if (!performLayout)
            {
                CommonProperties.xClearPreferredSizeCache(this);
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        this.LayoutEngine.InitLayout(collection1[num1], BoundsSpecified.All);
                        CommonProperties.xClearPreferredSizeCache(collection1[num1]);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected ContentAlignment RtlTranslateAlignment(ContentAlignment align)
        {
            return this.RtlTranslateContent(align);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected HorizontalAlignment RtlTranslateAlignment(HorizontalAlignment align)
        {
            return this.RtlTranslateHorizontal(align);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected LeftRightAlignment RtlTranslateAlignment(LeftRightAlignment align)
        {
            return this.RtlTranslateLeftRight(align);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal ContentAlignment RtlTranslateContent(ContentAlignment align)
        {
            if (System.Windows.Forms.RightToLeft.Yes != this.RightToLeft)
            {
                return align;
            }
            if ((align & WindowsFormsUtils.AnyTopAlign) != ((ContentAlignment) 0))
            {
                ContentAlignment alignment1 = align;
                if (alignment1 != ContentAlignment.TopLeft)
                {
                    if (alignment1 == ContentAlignment.TopRight)
                    {
                        return ContentAlignment.TopLeft;
                    }
                }
                else
                {
                    return ContentAlignment.TopRight;
                }
            }
            if ((align & WindowsFormsUtils.AnyMiddleAlign) != ((ContentAlignment) 0))
            {
                ContentAlignment alignment2 = align;
                if (alignment2 != ContentAlignment.MiddleLeft)
                {
                    if (alignment2 == ContentAlignment.MiddleRight)
                    {
                        return ContentAlignment.MiddleLeft;
                    }
                }
                else
                {
                    return ContentAlignment.MiddleRight;
                }
            }
            if ((align & WindowsFormsUtils.AnyBottomAlign) == ((ContentAlignment) 0))
            {
                return align;
            }
            ContentAlignment alignment3 = align;
            if (alignment3 != ContentAlignment.BottomLeft)
            {
                if (alignment3 == ContentAlignment.BottomRight)
                {
                    return ContentAlignment.BottomLeft;
                }
                return align;
            }
            return ContentAlignment.BottomRight;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected HorizontalAlignment RtlTranslateHorizontal(HorizontalAlignment align)
        {
            if (System.Windows.Forms.RightToLeft.Yes == this.RightToLeft)
            {
                if (align == HorizontalAlignment.Left)
                {
                    return HorizontalAlignment.Right;
                }
                if (HorizontalAlignment.Right == align)
                {
                    return HorizontalAlignment.Left;
                }
            }
            return align;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected LeftRightAlignment RtlTranslateLeftRight(LeftRightAlignment align)
        {
            if (System.Windows.Forms.RightToLeft.Yes == this.RightToLeft)
            {
                if (align == LeftRightAlignment.Left)
                {
                    return LeftRightAlignment.Right;
                }
                if (LeftRightAlignment.Right == align)
                {
                    return LeftRightAlignment.Left;
                }
            }
            return align;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Scale(SizeF factor)
        {
            this.Scale(factor, factor, this);
        }

        [Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202"), EditorBrowsable(EditorBrowsableState.Never)]
        public void Scale(float ratio)
        {
            this.ScaleCore(ratio, ratio);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202")]
        public void Scale(float dx, float dy)
        {
            this.SuspendLayout();
            try
            {
                this.ScaleCore(dx, dy);
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        internal virtual void Scale(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
        {
            using (LayoutTransaction transaction1 = new LayoutTransaction(this, this, PropertyNames.Bounds, false))
            {
                this.ScaleControl(includedFactor, excludedFactor, requestingControl);
                this.ScaleChildControls(includedFactor, excludedFactor, requestingControl);
            }
            LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
        }

        internal void ScaleChildControls(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
        {
            if (this.ScaleChildren)
            {
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        Control control1 = collection1[num1];
                        control1.Scale(includedFactor, excludedFactor, requestingControl);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            System.Windows.Forms.CreateParams params1 = this.CreateParams;
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT(0, 0, 0, 0);
            System.Windows.Forms.SafeNativeMethods.AdjustWindowRectEx(ref rect1, params1.Style, this.HasMenu, params1.ExStyle);
            System.Drawing.Size size1 = this.MinimumSize;
            System.Drawing.Size size2 = this.MaximumSize;
            this.MinimumSize = System.Drawing.Size.Empty;
            this.MaximumSize = System.Drawing.Size.Empty;
            Rectangle rectangle1 = this.GetScaledBounds(this.Bounds, factor, specified);
            float single1 = factor.Width;
            float single2 = factor.Height;
            System.Windows.Forms.Padding padding1 = this.Padding;
            System.Windows.Forms.Padding padding2 = this.Margin;
            if (single1 == 1f)
            {
                specified &= ((BoundsSpecified) (-6));
            }
            if (single2 == 1f)
            {
                specified &= ((BoundsSpecified) (-11));
            }
            if (single1 != 1f)
            {
                padding1.Left = (int) Math.Round((double) (padding1.Left * single1));
                padding1.Right = (int) Math.Round((double) (padding1.Right * single1));
                padding2.Left = (int) Math.Round((double) (padding2.Left * single1));
                padding2.Right = (int) Math.Round((double) (padding2.Right * single1));
            }
            if (single2 != 1f)
            {
                padding1.Top = (int) Math.Round((double) (padding1.Top * single2));
                padding1.Bottom = (int) Math.Round((double) (padding1.Bottom * single2));
                padding2.Top = (int) Math.Round((double) (padding2.Top * single2));
                padding2.Bottom = (int) Math.Round((double) (padding2.Bottom * single2));
            }
            this.Padding = padding1;
            this.Margin = padding2;
            System.Drawing.Size size3 = rect1.Size;
            if (!size1.IsEmpty)
            {
                size1 -= size3;
                size1 = this.ScaleSize(LayoutUtils.UnionSizes(System.Drawing.Size.Empty, size1), factor.Width, factor.Height) + size3;
            }
            if (!size2.IsEmpty)
            {
                size2 -= size3;
                size2 = this.ScaleSize(LayoutUtils.UnionSizes(System.Drawing.Size.Empty, size2), factor.Width, factor.Height) + size3;
            }
            System.Drawing.Size size4 = LayoutUtils.ConvertZeroToUnbounded(size2);
            System.Drawing.Size size5 = LayoutUtils.IntersectSizes(rectangle1.Size, size4);
            size5 = LayoutUtils.UnionSizes(size5, size1);
            this.SetBoundsCore(rectangle1.X, rectangle1.Y, size5.Width, size5.Height, BoundsSpecified.All);
            this.MaximumSize = size2;
            this.MinimumSize = size1;
        }

        internal void ScaleControl(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
        {
            BoundsSpecified specified1 = BoundsSpecified.None;
            BoundsSpecified specified2 = BoundsSpecified.None;
            if (!includedFactor.IsEmpty)
            {
                specified1 = this.RequiredScaling;
            }
            if (!excludedFactor.IsEmpty)
            {
                specified2 |= (~this.RequiredScaling & BoundsSpecified.All);
            }
            if (specified1 != BoundsSpecified.None)
            {
                this.ScaleControl(includedFactor, specified1);
            }
            if (specified2 != BoundsSpecified.None)
            {
                this.ScaleControl(excludedFactor, specified2);
            }
            if (!includedFactor.IsEmpty)
            {
                this.RequiredScaling = BoundsSpecified.None;
            }
        }

        [Obsolete("This method has been deprecated. Use the ScaleChildren and ScaleControl methods instead. http://go.microsoft.com/fwlink/?linkid=14202"), EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void ScaleCore(float dx, float dy)
        {
            this.SuspendLayout();
            try
            {
                int num1 = (int) Math.Round((double) (this.x * dx));
                int num2 = (int) Math.Round((double) (this.y * dy));
                int num3 = this.width;
                if ((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth)
                {
                    num3 = ((int) Math.Round((double) ((this.x + this.width) * dx))) - num1;
                }
                int num4 = this.height;
                if ((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight)
                {
                    num4 = ((int) Math.Round((double) ((this.y + this.height) * dy))) - num2;
                }
                this.SetBounds(num1, num2, num3, num4, BoundsSpecified.All);
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 == null)
                {
                    return;
                }
                for (int num5 = 0; num5 < collection1.Count; num5++)
                {
                    collection1[num5].Scale(dx, dy);
                }
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        internal System.Drawing.Size ScaleSize(System.Drawing.Size startSize, float x, float y)
        {
            System.Drawing.Size size1 = startSize;
            if (!this.GetStyle(ControlStyles.FixedWidth))
            {
                size1.Width = (int) Math.Round((double) (size1.Width * x));
            }
            if (!this.GetStyle(ControlStyles.FixedHeight))
            {
                size1.Height = (int) Math.Round((double) (size1.Height * y));
            }
            return size1;
        }

        public void Select()
        {
            this.Select(false, false);
        }

        protected virtual void Select(bool directed, bool forward)
        {
            IContainerControl control1 = this.GetContainerControlInternal();
            if (control1 != null)
            {
                control1.ActiveControl = this;
            }
        }

        public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
        {
            if (!this.Contains(ctl) || (!nested && (ctl.parent != this)))
            {
                ctl = null;
            }
            bool flag1 = false;
            Control control1 = ctl;
            do
            {
                ctl = this.GetNextControl(ctl, forward);
                if (ctl == null)
                {
                    if (!wrap)
                    {
                        break;
                    }
                    if (flag1)
                    {
                        return false;
                    }
                    flag1 = true;
                }
                else if ((ctl.CanSelect && (!tabStopOnly || ctl.TabStop)) && (nested || (ctl.parent == this)))
                {
                    ctl.Select(true, forward);
                    return true;
                }
            }
            while (ctl != control1);
            return false;
        }

        internal bool SelectNextControlInternal(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
        {
            System.Windows.Forms.IntSecurity.ModifyFocus.Assert();
            return this.SelectNextControl(ctl, forward, tabStopOnly, nested, wrap);
        }

        private void SelectNextIfFocused()
        {
            if (this.ContainsFocus && (this.ParentInternal != null))
            {
                IContainerControl control1 = this.ParentInternal.GetContainerControlInternal();
                if (control1 != null)
                {
                    ((Control) control1).SelectNextControlInternal(this, true, true, true, true);
                }
            }
        }

        internal IntPtr SendMessage(int msg, bool wparam, int lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, int wparam, ref System.Windows.Forms.NativeMethods.RECT lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, out lparam);
        }

        internal IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, int wparam, IntPtr lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, (IntPtr) wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, int wparam, string lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, IntPtr wparam, int lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, (IntPtr) lparam);
        }

        internal IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, ref int wparam, ref int lparam)
        {
            return System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, ref wparam, ref lparam);
        }

        public void SendToBack()
        {
            if (this.parent != null)
            {
                this.parent.Controls.SetChildIndex(this, -1);
            }
            else if (this.IsHandleCreated && this.GetTopLevel())
            {
                System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), System.Windows.Forms.NativeMethods.HWND_BOTTOM, 0, 0, 0, 0, 3);
            }
        }

        internal void SetAcceptDrops(bool accept)
        {
            if ((accept != this.GetState(0x80)) && this.IsHandleCreated)
            {
                try
                {
                    if (Application.OleRequired() != ApartmentState.STA)
                    {
                        throw new ThreadStateException(System.Windows.Forms.SR.GetString("ThreadMustBeSTA"));
                    }
                    if (accept)
                    {
                        System.Windows.Forms.IntSecurity.ClipboardRead.Demand();
                        int num1 = System.Windows.Forms.UnsafeNativeMethods.RegisterDragDrop(new HandleRef(this, this.Handle), new DropTarget(this));
                        if ((num1 != 0) && (num1 != -2147221247))
                        {
                            throw new Win32Exception(num1);
                        }
                    }
                    else
                    {
                        int num2 = System.Windows.Forms.UnsafeNativeMethods.RevokeDragDrop(new HandleRef(this, this.Handle));
                        if ((num2 != 0) && (num2 != -2147221248))
                        {
                            throw new Win32Exception(num2);
                        }
                    }
                    this.SetState(0x80, accept);
                }
                catch (Exception exception1)
                {
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("DragDropRegFailed"), exception1);
                }
                catch
                {
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("DragDropRegFailed"));
                }
            }
        }

        protected void SetAutoSizeMode(AutoSizeMode mode)
        {
            CommonProperties.SetAutoSizeMode(this, mode);
        }

        public void SetBounds(int x, int y, int width, int height)
        {
            if (((this.x != x) || (this.y != y)) || ((this.width != width) || (this.height != height)))
            {
                this.SetBoundsCore(x, y, width, height, BoundsSpecified.All);
                LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            else
            {
                this.InitScaling(BoundsSpecified.All);
            }
        }

        public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
            {
                x = this.x;
            }
            if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
            {
                y = this.y;
            }
            if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
            {
                width = this.width;
            }
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
            {
                height = this.height;
            }
            if (((this.x != x) || (this.y != y)) || ((this.width != width) || (this.height != height)))
            {
                this.SetBoundsCore(x, y, width, height, specified);
                LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            else
            {
                this.InitScaling(specified);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.ParentInternal != null)
            {
                this.ParentInternal.SuspendLayout();
            }
            try
            {
                if (((this.x == x) && (this.y == y)) && ((this.width == width) && (this.height == height)))
                {
                    return;
                }
                CommonProperties.UpdateSpecifiedBounds(this, x, y, width, height, specified);
                Rectangle rectangle1 = this.ApplyBoundsConstraints(x, y, width, height);
                width = rectangle1.Width;
                height = rectangle1.Height;
                x = rectangle1.X;
                y = rectangle1.Y;
                if (!this.IsHandleCreated)
                {
                    this.UpdateBounds(x, y, width, height);
                }
                else
                {
                    if (this.GetState(0x10000))
                    {
                        return;
                    }
                    int num1 = 20;
                    if ((this.x == x) && (this.y == y))
                    {
                        num1 |= 2;
                    }
                    if ((this.width == width) && (this.height == height))
                    {
                        num1 |= 1;
                    }
                    this.OnBoundsUpdate(x, y, width, height);
                    System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, x, y, width, height, num1);
                }
            }
            finally
            {
                this.InitScaling(specified);
                if (this.ParentInternal != null)
                {
                    CommonProperties.xClearPreferredSizeCache(this.ParentInternal);
                    this.ParentInternal.LayoutEngine.InitLayout(this, specified);
                    this.ParentInternal.ResumeLayout(true);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void SetClientSizeCore(int x, int y)
        {
            this.Size = this.SizeFromClientSize(x, y);
            this.clientWidth = x;
            this.clientHeight = y;
            this.OnClientSizeChanged(EventArgs.Empty);
        }

        private void SetHandle(IntPtr value)
        {
            if (value == IntPtr.Zero)
            {
                this.SetState(1, false);
            }
            this.UpdateRoot();
        }

        private void SetImeMode(System.Windows.Forms.ImeMode value)
        {
            if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, System.Windows.Forms.ImeMode.Inherit, System.Windows.Forms.ImeMode.Hangul))
            {
                throw new InvalidEnumArgumentException("ImeMode", (int) value, typeof(System.Windows.Forms.ImeMode));
            }
            System.Windows.Forms.ImeMode mode1 = this.CachedImeMode;
            this.Properties.SetInteger(Control.PropImeMode, (int) value);
            if (!base.DesignMode && (this.CachedImeMode != System.Windows.Forms.ImeMode.NoControl))
            {
                if (this.Focused)
                {
                    this.CurrentImeContextMode = this.CachedImeMode;
                }
                else if (this.ContainsFocus)
                {
                    Control control1 = Control.FromChildHandleInternal(System.Windows.Forms.UnsafeNativeMethods.GetFocus());
                    control1.CurrentImeContextMode = control1.CachedImeMode;
                }
            }
            if (this.CachedImeMode != mode1)
            {
                this.OnImeModeChanged(EventArgs.Empty);
            }
        }

        private void SetParentHandle(IntPtr value)
        {
            if (this.IsHandleCreated)
            {
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.Handle));
                if ((ptr1 != value) || ((ptr1 == IntPtr.Zero) && !this.GetTopLevel()))
                {
                    bool flag1 = this.GetTopLevel();
                    bool flag2 = ((ptr1 == IntPtr.Zero) && !flag1) || ((value == IntPtr.Zero) && flag1);
                    if (flag2)
                    {
                        Form form1 = this as Form;
                        if ((form1 != null) && !form1.CanRecreateHandle())
                        {
                            flag2 = false;
                            this.UpdateStyles();
                        }
                    }
                    if (flag2)
                    {
                        this.RecreateHandle();
                    }
                    if (!this.GetTopLevel())
                    {
                        if (value == IntPtr.Zero)
                        {
                            Application.ParkHandle(new HandleRef(this.window, this.Handle));
                            this.UpdateRoot();
                        }
                        else
                        {
                            System.Windows.Forms.UnsafeNativeMethods.SetParent(new HandleRef(this.window, this.Handle), new HandleRef(null, value));
                            if (this.parent != null)
                            {
                                this.parent.UpdateChildZOrder(this);
                            }
                            Application.UnparkHandle(new HandleRef(this.window, this.Handle));
                        }
                    }
                }
            }
        }

        internal void SetState(int flag, bool value)
        {
            this.state = value ? (this.state | flag) : (this.state & ~flag);
        }

        internal void SetState2(int flag, bool value)
        {
            this.state2 = value ? (this.state2 | flag) : (this.state2 & ~flag);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void SetStyle(ControlStyles flag, bool value)
        {
            if (((flag & ControlStyles.EnableNotifyMessage) != ((ControlStyles) 0)) && value)
            {
                System.Windows.Forms.IntSecurity.UnmanagedCode.Demand();
            }
            this.controlStyle = value ? (this.controlStyle | flag) : (this.controlStyle & ~flag);
        }

        protected void SetTopLevel(bool value)
        {
            if (value && this.IsActiveX)
            {
                throw new InvalidOperationException(System.Windows.Forms.SR.GetString("TopLevelNotAllowedIfActiveX"));
            }
            if (value)
            {
                if (this is Form)
                {
                    System.Windows.Forms.IntSecurity.TopLevelWindow.Demand();
                }
                else
                {
                    System.Windows.Forms.IntSecurity.UnrestrictedWindows.Demand();
                }
            }
            this.SetTopLevelInternal(value);
        }

        internal void SetTopLevelInternal(bool value)
        {
            if (this.GetTopLevel() != value)
            {
                if (this.parent != null)
                {
                    throw new ArgumentException(System.Windows.Forms.SR.GetString("TopLevelParentedControl"), "value");
                }
                this.SetState(0x80000, value);
                if (this.IsHandleCreated && this.GetState2(8))
                {
                    this.ListenToUserPreferenceChanged(value);
                }
                this.UpdateStyles();
                this.SetParentHandle(IntPtr.Zero);
                if (value && this.Visible)
                {
                    this.CreateControl();
                }
                this.UpdateRoot();
            }
        }

        private void SetUnrestrictedImeMode(System.Windows.Forms.ImeMode value, bool forceImeMode)
        {
            if (this.CanEnableIme || forceImeMode)
            {
                this.Properties.SetInteger(Control.PropUnrestrictedImeMode, (int) value);
            }
        }

        internal static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
        {
            IntPtr ptr1 = Graphics.GetHalftonePalette();
            IntPtr ptr2 = System.Windows.Forms.SafeNativeMethods.SelectPalette(new HandleRef(null, dc), new HandleRef(null, ptr1), force ? 0 : 1);
            if ((ptr2 != IntPtr.Zero) && realizePalette)
            {
                System.Windows.Forms.SafeNativeMethods.RealizePalette(new HandleRef(null, dc));
            }
            return ptr2;
        }

        protected virtual void SetVisibleCore(bool value)
        {
            try
            {
                System.Internal.HandleCollector.SuspendCollect();
                if (this.GetVisibleCore() != value)
                {
                    if (!value)
                    {
                        this.SelectNextIfFocused();
                    }
                    bool flag1 = false;
                    if (this.GetTopLevel())
                    {
                        if (this.IsHandleCreated || value)
                        {
                            System.Windows.Forms.SafeNativeMethods.ShowWindow(new HandleRef(this, this.Handle), value ? this.ShowParams : 0);
                        }
                    }
                    else if (this.IsHandleCreated || ((value && (this.parent != null)) && this.parent.Created))
                    {
                        this.SetState(2, value);
                        flag1 = true;
                        try
                        {
                            if (value)
                            {
                                this.CreateControl();
                            }
                            System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, 0, 0, 0, 0, 0x17 | (value ? 0x40 : 0x80));
                        }
                        catch
                        {
                            this.SetState(2, !value);
                            throw;
                        }
                    }
                    if (this.GetVisibleCore() != value)
                    {
                        this.SetState(2, value);
                        flag1 = true;
                    }
                    if (flag1)
                    {
                        using (LayoutTransaction transaction1 = new LayoutTransaction(this.parent, this, PropertyNames.Visible))
                        {
                            this.OnVisibleChanged(EventArgs.Empty);
                        }
                    }
                    this.UpdateRoot();
                }
                else
                {
                    if ((!this.GetState(2) && !value) && (this.IsHandleCreated && !System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))))
                    {
                        return;
                    }
                    this.SetState(2, value);
                    if (this.IsHandleCreated)
                    {
                        System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, 0, 0, 0, 0, 0x17 | (value ? 0x40 : 0x80));
                    }
                }
            }
            finally
            {
                System.Internal.HandleCollector.ResumeCollect();
            }
        }

        private void SetWindowFont()
        {
            this.SendMessage(0x30, this.FontHandle, 0);
        }

        private void SetWindowStyle(int flag, bool value)
        {
            int num1 = (int) ((long) System.Windows.Forms.UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16));
            System.Windows.Forms.UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, value ? ((IntPtr) (num1 | flag)) : ((IntPtr) (num1 & ~flag))));
        }

        private bool ShouldSerializeAccessibleName()
        {
            string text1 = this.AccessibleName;
            if (text1 != null)
            {
                return (text1.Length > 0);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeBackColor()
        {
            System.Drawing.Color color1 = this.Properties.GetColor(Control.PropBackColor);
            return !color1.IsEmpty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeCursor()
        {
            bool flag1;
            object obj1 = this.Properties.GetObject(Control.PropCursor, out flag1);
            if (flag1)
            {
                return (obj1 != null);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeEnabled()
        {
            return !this.GetState(4);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeFont()
        {
            bool flag1;
            object obj1 = this.Properties.GetObject(Control.PropFont, out flag1);
            if (flag1)
            {
                return (obj1 != null);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeForeColor()
        {
            System.Drawing.Color color1 = this.Properties.GetColor(Control.PropForeColor);
            return !color1.IsEmpty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeImeMode()
        {
            bool flag1;
            int num1 = this.Properties.GetInteger(Control.PropImeMode, out flag1);
            if (flag1)
            {
                return (((System.Windows.Forms.ImeMode) num1) != this.DefaultImeMode);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializeMargin()
        {
            return !this.Margin.Equals(this.DefaultMargin);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeMaximumSize()
        {
            return (this.MaximumSize != this.DefaultMaximumSize);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeMinimumSize()
        {
            return (this.MinimumSize != this.DefaultMinimumSize);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializePadding()
        {
            return !this.Padding.Equals(this.DefaultPadding);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeRightToLeft()
        {
            bool flag1;
            int num1 = this.Properties.GetInteger(Control.PropRightToLeft, out flag1);
            if (flag1)
            {
                return (num1 != 2);
            }
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeSize()
        {
            System.Drawing.Size size1 = this.DefaultSize;
            if (this.width == size1.Width)
            {
                return (this.height != size1.Height);
            }
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeText()
        {
            return (this.Text.Length != 0);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeVisible()
        {
            return !this.GetState(2);
        }

        public void Show()
        {
            this.Visible = true;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)
        {
            return this.SizeFromClientSize(clientSize.Width, clientSize.Height);
        }

        internal System.Drawing.Size SizeFromClientSize(int width, int height)
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT(0, 0, width, height);
            System.Windows.Forms.CreateParams params1 = this.CreateParams;
            System.Windows.Forms.SafeNativeMethods.AdjustWindowRectEx(ref rect1, params1.Style, this.HasMenu, params1.ExStyle);
            return rect1.Size;
        }

        public void SuspendLayout()
        {
            this.layoutSuspendCount = (byte) (this.layoutSuspendCount + 1);
            if (this.layoutSuspendCount == 1)
            {
                this.OnLayoutSuspended();
            }
        }

        void IDropTarget.OnDragDrop(DragEventArgs drgEvent)
        {
            this.OnDragDrop(drgEvent);
        }

        void IDropTarget.OnDragEnter(DragEventArgs drgEvent)
        {
            this.OnDragEnter(drgEvent);
        }

        void IDropTarget.OnDragLeave(EventArgs e)
        {
            this.OnDragLeave(e);
        }

        void IDropTarget.OnDragOver(DragEventArgs drgEvent)
        {
            this.OnDragOver(drgEvent);
        }

        void ISupportOleDropSource.OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
        {
            this.OnGiveFeedback(giveFeedbackEventArgs);
        }

        void ISupportOleDropSource.OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEventArgs)
        {
            this.OnQueryContinueDrag(queryContinueDragEventArgs);
        }

        void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string affectedProperty)
        {
            this.PerformLayout(new LayoutEventArgs(affectedElement, affectedProperty));
        }

        void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
        {
            ISite site1 = this.Site;
            IComponentChangeService service1 = null;
            PropertyDescriptor descriptor1 = null;
            PropertyDescriptor descriptor2 = null;
            bool flag1 = false;
            bool flag2 = false;
            if ((site1 != null) && site1.DesignMode)
            {
                service1 = (IComponentChangeService) site1.GetService(typeof(IComponentChangeService));
                if (service1 != null)
                {
                    descriptor1 = TypeDescriptor.GetProperties(this)[PropertyNames.Size];
                    descriptor2 = TypeDescriptor.GetProperties(this)[PropertyNames.Location];
                    try
                    {
                        if (((descriptor1 != null) && !descriptor1.IsReadOnly) && ((bounds.Width != this.Width) || (bounds.Height != this.Height)))
                        {
                            if (!(site1 is INestedSite))
                            {
                                service1.OnComponentChanging(this, descriptor1);
                            }
                            flag1 = true;
                        }
                        if (((descriptor2 != null) && !descriptor2.IsReadOnly) && ((bounds.X != this.x) || (bounds.Y != this.y)))
                        {
                            if (!(site1 is INestedSite))
                            {
                                service1.OnComponentChanging(this, descriptor2);
                            }
                            flag2 = true;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
            this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
            if ((site1 != null) && (service1 != null))
            {
                try
                {
                    if (flag1)
                    {
                        service1.OnComponentChanged(this, descriptor1, null, null);
                    }
                    if (flag2)
                    {
                        service1.OnComponentChanged(this, descriptor2, null, null);
                    }
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleControl.FreezeEvents(int bFreeze)
        {
            this.ActiveXInstance.EventsFrozen = bFreeze != 0;
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleControl.GetControlInfo(System.Windows.Forms.NativeMethods.tagCONTROLINFO pCI)
        {
            pCI.cb = Marshal.SizeOf(typeof(System.Windows.Forms.NativeMethods.tagCONTROLINFO));
            pCI.hAccel = IntPtr.Zero;
            pCI.cAccel = 0;
            pCI.dwFlags = 0;
            if (this.IsInputKey(Keys.Return))
            {
                pCI.dwFlags |= 1;
            }
            if (this.IsInputKey(Keys.Escape))
            {
                pCI.dwFlags |= 2;
            }
            this.ActiveXInstance.GetControlInfo(pCI);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleControl.OnAmbientPropertyChange(int dispID)
        {
            this.ActiveXInstance.OnAmbientPropertyChange(dispID);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleControl.OnMnemonic(ref System.Windows.Forms.NativeMethods.MSG pMsg)
        {
            bool flag1 = this.ProcessMnemonic((char) ((ushort) ((int) pMsg.wParam)));
            return 0;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.ContextSensitiveHelp(int fEnterMode)
        {
            ((System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject) this).ContextSensitiveHelp(fEnterMode);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.EnableModeless(int fEnable)
        {
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.GetWindow(out IntPtr hwnd)
        {
            return ((System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject) this).GetWindow(out hwnd);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.OnDocWindowActivate(int fActivate)
        {
            this.ActiveXInstance.OnDocWindowActivate(fActivate);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.OnFrameWindowActivate(int fActivate)
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.ResizeBorder(System.Windows.Forms.NativeMethods.COMRECT prcBorder, System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceUIWindow pUIWindow, bool fFrameWindow)
        {
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceActiveObject.TranslateAccelerator(ref System.Windows.Forms.NativeMethods.MSG lpmsg)
        {
            return this.ActiveXInstance.TranslateAccelerator(ref lpmsg);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.ContextSensitiveHelp(int fEnterMode)
        {
            if (fEnterMode != 0)
            {
                this.OnHelpRequested(new HelpEventArgs(Control.MousePosition));
            }
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.GetWindow(out IntPtr hwnd)
        {
            return this.ActiveXInstance.GetWindow(out hwnd);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.InPlaceDeactivate()
        {
            this.ActiveXInstance.InPlaceDeactivate();
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.ReactivateAndUndo()
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.SetObjectRects(System.Windows.Forms.NativeMethods.COMRECT lprcPosRect, System.Windows.Forms.NativeMethods.COMRECT lprcClipRect)
        {
            this.ActiveXInstance.SetObjectRects(lprcPosRect, lprcClipRect);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject.UIDeactivate()
        {
            return this.ActiveXInstance.UIDeactivate();
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.Advise(IAdviseSink pAdvSink, out int cookie)
        {
            cookie = this.ActiveXInstance.Advise(pAdvSink);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.Close(int dwSaveOption)
        {
            this.ActiveXInstance.Close(dwSaveOption);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.DoVerb(int iVerb, IntPtr lpmsg, System.Windows.Forms.UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, System.Windows.Forms.NativeMethods.COMRECT lprcPosRect)
        {
            short num1 = (short) iVerb;
            iVerb = num1;
            try
            {
                this.ActiveXInstance.DoVerb(iVerb, lpmsg, pActiveSite, lindex, hwndParent, lprcPosRect);
            }
            catch (Exception)
            {
                throw;
            }
            catch
            {
                throw;
            }
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.EnumAdvise(out IEnumSTATDATA e)
        {
            e = null;
            return -2147467263;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.EnumVerbs(out System.Windows.Forms.UnsafeNativeMethods.IEnumOLEVERB e)
        {
            return ActiveXImpl.EnumVerbs(out e);
        }

        System.Windows.Forms.UnsafeNativeMethods.IOleClientSite System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetClientSite()
        {
            return this.ActiveXInstance.GetClientSite();
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetClipboardData(int dwReserved, out System.Runtime.InteropServices.ComTypes.IDataObject data)
        {
            data = null;
            return -2147467263;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetExtent(int dwDrawAspect, System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
        {
            this.ActiveXInstance.GetExtent(dwDrawAspect, pSizel);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetMiscStatus(int dwAspect, out int cookie)
        {
            if ((dwAspect & 1) != 0)
            {
                int num1 = 0x20180;
                if (this.GetStyle(ControlStyles.ResizeRedraw))
                {
                    num1 |= 1;
                }
                if (this is IButtonControl)
                {
                    num1 |= 0x1000;
                }
                cookie = num1;
                return 0;
            }
            cookie = 0;
            return -2147221397;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
        {
            moniker = null;
            return -2147467263;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetUserClassID(ref Guid pClsid)
        {
            pClsid = base.GetType().GUID;
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.GetUserType(int dwFormOfType, out string userType)
        {
            if (dwFormOfType == 1)
            {
                userType = base.GetType().FullName;
            }
            else
            {
                userType = base.GetType().Name;
            }
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.InitFromData(System.Runtime.InteropServices.ComTypes.IDataObject pDataObject, int fCreation, int dwReserved)
        {
            return -2147467263;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.IsUpToDate()
        {
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.OleUpdate()
        {
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.SetClientSite(System.Windows.Forms.UnsafeNativeMethods.IOleClientSite pClientSite)
        {
            this.ActiveXInstance.SetClientSite(pClientSite);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.SetColorScheme(System.Windows.Forms.NativeMethods.tagLOGPALETTE pLogpal)
        {
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.SetExtent(int dwDrawAspect, System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
        {
            this.ActiveXInstance.SetExtent(dwDrawAspect, pSizel);
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.SetHostNames(string szContainerApp, string szContainerObj)
        {
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.SetMoniker(int dwWhichMoniker, object pmk)
        {
            return -2147467263;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleObject.Unadvise(int dwConnection)
        {
            this.ActiveXInstance.Unadvise(dwConnection);
            return 0;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
        {
            ((System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject) this).ContextSensitiveHelp(fEnterMode);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
        {
            return ((System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceObject) this).GetWindow(out hwnd);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersist.GetClassID(out Guid pClassID)
        {
            pClassID = base.GetType().GUID;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistPropertyBag.GetClassID(out Guid pClassID)
        {
            pClassID = base.GetType().GUID;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistPropertyBag.InitNew()
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistPropertyBag.Load(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag pPropBag, System.Windows.Forms.UnsafeNativeMethods.IErrorLog pErrorLog)
        {
            this.ActiveXInstance.Load(pPropBag, pErrorLog);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistPropertyBag.Save(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
        {
            this.ActiveXInstance.Save(pPropBag, fClearDirty, fSaveAllProperties);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.GetClassID(out Guid pClassID)
        {
            pClassID = base.GetType().GUID;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.HandsOffStorage()
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.InitNew(System.Windows.Forms.UnsafeNativeMethods.IStorage pstg)
        {
        }

        int System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.IsDirty()
        {
            return this.ActiveXInstance.IsDirty();
        }

        int System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.Load(System.Windows.Forms.UnsafeNativeMethods.IStorage pstg)
        {
            this.ActiveXInstance.Load(pstg);
            return 0;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.Save(System.Windows.Forms.UnsafeNativeMethods.IStorage pstg, bool fSameAsLoad)
        {
            this.ActiveXInstance.Save(pstg, fSameAsLoad);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStorage.SaveCompleted(System.Windows.Forms.UnsafeNativeMethods.IStorage pStgNew)
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.GetClassID(out Guid pClassID)
        {
            pClassID = base.GetType().GUID;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.GetSizeMax(long pcbSize)
        {
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.InitNew()
        {
        }

        int System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.IsDirty()
        {
            return this.ActiveXInstance.IsDirty();
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.Load(System.Windows.Forms.UnsafeNativeMethods.IStream pstm)
        {
            this.ActiveXInstance.Load(pstm);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IPersistStreamInit.Save(System.Windows.Forms.UnsafeNativeMethods.IStream pstm, bool fClearDirty)
        {
            this.ActiveXInstance.Save(pstm, fClearDirty);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IQuickActivate.GetContentExtent(System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
        {
            this.ActiveXInstance.GetExtent(1, pSizel);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IQuickActivate.QuickActivate(System.Windows.Forms.UnsafeNativeMethods.tagQACONTAINER pQaContainer, System.Windows.Forms.UnsafeNativeMethods.tagQACONTROL pQaControl)
        {
            this.ActiveXInstance.QuickActivate(pQaContainer, pQaControl);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IQuickActivate.SetContentExtent(System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
        {
            this.ActiveXInstance.SetExtent(1, pSizel);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, System.Windows.Forms.NativeMethods.COMRECT lprcBounds, System.Windows.Forms.NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
        {
            try
            {
                this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
            }
            catch (ExternalException exception1)
            {
                return exception1.ErrorCode;
            }
            return 0;
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
        {
            return -2147467263;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
        {
            this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, System.Windows.Forms.NativeMethods.tagLOGPALETTE ppColorSet)
        {
            return -2147467263;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
        {
            this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject.Unfreeze(int dwFreeze)
        {
            return -2147467263;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject2.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, System.Windows.Forms.NativeMethods.COMRECT lprcBounds, System.Windows.Forms.NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
        {
            this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject2.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
        {
            return -2147467263;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject2.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
        {
            this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject2.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, System.Windows.Forms.NativeMethods.tagLOGPALETTE ppColorSet)
        {
            return -2147467263;
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject2.GetExtent(int dwDrawAspect, int lindex, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, System.Windows.Forms.NativeMethods.tagSIZEL lpsizel)
        {
            ((System.Windows.Forms.UnsafeNativeMethods.IOleObject) this).GetExtent(dwDrawAspect, lpsizel);
        }

        void System.Windows.Forms.UnsafeNativeMethods.IViewObject2.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
        {
            this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
        }

        int System.Windows.Forms.UnsafeNativeMethods.IViewObject2.Unfreeze(int dwFreeze)
        {
            return -2147467263;
        }

        private void UnhookMouseEvent()
        {
            this.SetState(0x4000, false);
        }

        public void Update()
        {
            System.Windows.Forms.SafeNativeMethods.UpdateWindow(new HandleRef(this.window, this.InternalHandle));
        }

        private void UpdateBindings()
        {
            for (int num1 = 0; num1 < this.DataBindings.Count; num1++)
            {
                System.Windows.Forms.BindingContext.UpdateBinding(this.BindingContext, this.DataBindings[num1]);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal void UpdateBounds()
        {
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
            System.Windows.Forms.UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), out rect1);
            int num1 = rect1.right;
            int num2 = rect1.bottom;
            System.Windows.Forms.UnsafeNativeMethods.GetWindowRect(new HandleRef(this.window, this.InternalHandle), out rect1);
            if (!this.GetTopLevel())
            {
                System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(System.Windows.Forms.NativeMethods.NullHandleRef, new HandleRef(null, System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle))), out rect1, 2);
            }
            this.UpdateBounds(rect1.left, rect1.top, rect1.right - rect1.left, rect1.bottom - rect1.top, num1, num2);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void UpdateBounds(int x, int y, int width, int height)
        {
            int num3;
            int num4;
            int num5;
            System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
            rect1.bottom = num3 = 0;
            rect1.top = num4 = num3;
            rect1.right = num5 = num4;
            rect1.left = num5;
            System.Windows.Forms.CreateParams params1 = this.CreateParams;
            System.Windows.Forms.SafeNativeMethods.AdjustWindowRectEx(ref rect1, params1.Style, false, params1.ExStyle);
            int num1 = width - (rect1.right - rect1.left);
            int num2 = height - (rect1.bottom - rect1.top);
            this.UpdateBounds(x, y, width, height, num1, num2);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
        {
            bool flag1 = (this.x != x) || (this.y != y);
            bool flag2 = (((this.Width != width) || (this.Height != height)) || (this.clientWidth != clientWidth)) || (this.clientHeight != clientHeight);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.clientWidth = clientWidth;
            this.clientHeight = clientHeight;
            if (flag1)
            {
                this.OnLocationChanged(EventArgs.Empty);
            }
            if (flag2)
            {
                this.OnSizeChanged(EventArgs.Empty);
                this.OnClientSizeChanged(EventArgs.Empty);
                CommonProperties.xClearPreferredSizeCache(this);
                LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
        }

        internal void UpdateCachedImeMode(IntPtr handle)
        {
            this.UpdateCachedImeMode(handle, false);
        }

        internal void UpdateCachedImeMode(IntPtr handle, bool force)
        {
            if (!this.UpdatingCachedIme)
            {
                System.Windows.Forms.ImeMode mode1 = this.CachedImeMode;
                if ((mode1 != System.Windows.Forms.ImeMode.NoControl) || force)
                {
                    System.Windows.Forms.ImeMode mode2 = ImeContext.GetImeMode(handle);
                    if (mode2 != System.Windows.Forms.ImeMode.Inherit)
                    {
                        this.UpdatingCachedIme = true;
                        try
                        {
                            this.Properties.SetInteger(Control.PropImeMode, (int) mode2);
                            if (mode2 != mode1)
                            {
                                this.OnImeModeChanged(EventArgs.Empty);
                            }
                        }
                        finally
                        {
                            this.UpdatingCachedIme = false;
                        }
                    }
                }
            }
        }

        private void UpdateChildControlIndex(Control ctl)
        {
            int num1 = 0;
            int num2 = this.Controls.GetChildIndex(ctl);
            IntPtr ptr1 = ctl.InternalHandle;
            while ((ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(null, ptr1), 3)) != IntPtr.Zero)
            {
                Control control1 = Control.FromHandleInternal(ptr1);
                if (control1 != null)
                {
                    num1 = this.Controls.GetChildIndex(control1, false) + 1;
                    break;
                }
            }
            if (num1 > num2)
            {
                num1--;
            }
            if (num1 != num2)
            {
                this.Controls.SetChildIndex(ctl, num1);
            }
        }

        private void UpdateChildZOrder(Control ctl)
        {
            if ((this.IsHandleCreated && ctl.IsHandleCreated) && (ctl.parent == this))
            {
                IntPtr ptr1 = (IntPtr) System.Windows.Forms.NativeMethods.HWND_TOP;
                int num1 = this.Controls.GetChildIndex(ctl);
                while (--num1 >= 0)
                {
                    Control control1 = this.Controls[num1];
                    if (control1.IsHandleCreated && (control1.parent == this))
                    {
                        ptr1 = control1.Handle;
                        break;
                    }
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(ctl.window, ctl.Handle), 3) != ptr1)
                {
                    this.state |= 0x100;
                    try
                    {
                        System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(ctl.window, ctl.Handle), new HandleRef(null, ptr1), 0, 0, 0, 0, 3);
                    }
                    finally
                    {
                        this.state &= -257;
                    }
                }
            }
        }

        private void UpdateReflectParent(bool findNewParent)
        {
            if ((!this.Disposing && findNewParent) && this.IsHandleCreated)
            {
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
                if (ptr1 != IntPtr.Zero)
                {
                    this.ReflectParent = Control.FromHandleInternal(ptr1);
                    return;
                }
            }
            this.ReflectParent = null;
        }

        private void UpdateRoot()
        {
            this.window.LockReference(this.GetTopLevel() && this.Visible);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void UpdateStyles()
        {
            this.UpdateStylesCore();
            this.OnStyleChanged(EventArgs.Empty);
        }

        internal virtual void UpdateStylesCore()
        {
            if (this.IsHandleCreated)
            {
                System.Windows.Forms.CreateParams params1 = this.CreateParams;
                int num1 = this.WindowStyle;
                int num2 = this.WindowExStyle;
                if ((this.state & 2) != 0)
                {
                    params1.Style |= 0x10000000;
                }
                if (num1 != params1.Style)
                {
                    this.WindowStyle = params1.Style;
                }
                if (num2 != params1.ExStyle)
                {
                    this.WindowExStyle = params1.ExStyle;
                    this.SetState(0x40000000, (params1.ExStyle & 0x400000) != 0);
                }
                System.Windows.Forms.SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), System.Windows.Forms.NativeMethods.NullHandleRef, 0, 0, 0, 0, 0x37);
                this.Invalidate(true);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void UpdateZOrder()
        {
            if (this.parent != null)
            {
                this.parent.UpdateChildZOrder(this);
            }
        }

        private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
        {
            if (pref.Category == UserPreferenceCategory.Color)
            {
                Control.defaultFont = null;
                this.OnSystemColorsChanged(EventArgs.Empty);
            }
        }

        internal bool ValidateActiveControl(out bool validatedControlAllowsFocusChange)
        {
            bool flag1 = true;
            validatedControlAllowsFocusChange = false;
            IContainerControl control1 = this.GetContainerControlInternal();
            if (control1 == null)
            {
                return flag1;
            }
            ContainerControl control2 = control1 as ContainerControl;
            if (control2 == null)
            {
                return flag1;
            }
            while (control2.ActiveControl == null)
            {
                Control control4 = control2.ParentInternal;
                if (control4 == null)
                {
                    break;
                }
                ContainerControl control3 = control4.GetContainerControlInternal() as ContainerControl;
                if (control3 == null)
                {
                    break;
                }
                control2 = control3;
            }
            return control2.ValidateInternal(true, out validatedControlAllowsFocusChange);
        }

        internal void WindowAssignHandle(IntPtr handle, bool value)
        {
            this.window.AssignHandle(handle, value);
        }

        internal void WindowReleaseHandle()
        {
            this.window.ReleaseHandle();
        }

        private void WmCaptureChanged(ref Message m)
        {
            this.OnMouseCaptureChanged(EventArgs.Empty);
            this.DefWndProc(ref m);
        }

        private void WmClose(ref Message m)
        {
            if (this.ParentInternal != null)
            {
                IntPtr ptr1 = this.Handle;
                IntPtr ptr2 = ptr1;
                while (ptr1 != IntPtr.Zero)
                {
                    ptr2 = ptr1;
                    ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(null, ptr1));
                    int num1 = (int) ((long) System.Windows.Forms.UnsafeNativeMethods.GetWindowLong(new HandleRef(null, ptr2), -16));
                    if ((num1 & 0x40000000) == 0)
                    {
                        break;
                    }
                }
                if (ptr2 != IntPtr.Zero)
                {
                    System.Windows.Forms.UnsafeNativeMethods.PostMessage(new HandleRef(null, ptr2), 0x10, IntPtr.Zero, IntPtr.Zero);
                }
            }
            this.DefWndProc(ref m);
        }

        private void WmCommand(ref Message m)
        {
            if (IntPtr.Zero == m.LParam)
            {
                if (Command.DispatchID(System.Windows.Forms.NativeMethods.Util.LOWORD(m.WParam)))
                {
                    return;
                }
            }
            else if (Control.ReflectMessageInternal(m.LParam, ref m))
            {
                return;
            }
            this.DefWndProc(ref m);
        }

        private void WmContextMenu(ref Message m)
        {
            System.Windows.Forms.ContextMenu menu1 = this.Properties.GetObject(Control.PropContextMenu) as System.Windows.Forms.ContextMenu;
            System.Windows.Forms.ContextMenuStrip strip1 = (menu1 != null) ? null : (this.Properties.GetObject(Control.PropContextMenuStrip) as System.Windows.Forms.ContextMenuStrip);
            if ((menu1 != null) || (strip1 != null))
            {
                Point point1;
                int num1 = System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam);
                int num2 = System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam);
                bool flag1 = false;
                if (((int) ((long) m.LParam)) == -1)
                {
                    flag1 = true;
                    point1 = new Point(this.Width / 2, this.Height / 2);
                }
                else
                {
                    point1 = this.PointToClientInternal(new Point(num1, num2));
                }
                if (this.ClientRectangle.Contains(point1))
                {
                    if (menu1 != null)
                    {
                        menu1.Show(this, point1);
                    }
                    else if (strip1 != null)
                    {
                        strip1.ShowInternal(this, point1, flag1);
                    }
                    else
                    {
                        this.DefWndProc(ref m);
                    }
                }
                else
                {
                    this.DefWndProc(ref m);
                }
            }
            else
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmCreate(ref Message m)
        {
            this.DefWndProc(ref m);
            if (this.parent != null)
            {
                this.parent.UpdateChildZOrder(this);
            }
            this.UpdateBounds();
            this.OnHandleCreated(EventArgs.Empty);
            if (!this.GetStyle(ControlStyles.CacheText))
            {
                this.text = null;
            }
        }

        private void WmCtlColorControl(ref Message m)
        {
            Control control1 = Control.FromHandleInternal(m.LParam);
            if (control1 != null)
            {
                m.Result = control1.InitializeDCForWmCtlColor(m.WParam, m.Msg);
                if (m.Result != IntPtr.Zero)
                {
                    return;
                }
            }
            this.DefWndProc(ref m);
        }

        private void WmDestroy(ref Message m)
        {
            if ((!this.RecreatingHandle && !this.Disposing) && (!this.IsDisposed && this.GetState(0x4000)))
            {
                this.OnMouseLeave(EventArgs.Empty);
                this.UnhookMouseEvent();
            }
            this.OnHandleDestroyed(EventArgs.Empty);
            if (!this.Disposing)
            {
                if (!this.RecreatingHandle)
                {
                    this.SetState(1, false);
                }
            }
            else
            {
                this.SetState(2, false);
            }
            this.DefWndProc(ref m);
        }

        private void WmDisplayChange(ref Message m)
        {
            BufferedGraphicsManager.Current.Invalidate();
            this.DefWndProc(ref m);
        }

        private void WmDrawItem(ref Message m)
        {
            if (m.WParam == IntPtr.Zero)
            {
                this.WmDrawItemMenuItem(ref m);
            }
            else
            {
                this.WmOwnerDraw(ref m);
            }
        }

        private void WmDrawItemMenuItem(ref Message m)
        {
            System.Windows.Forms.NativeMethods.DRAWITEMSTRUCT drawitemstruct1 = (System.Windows.Forms.NativeMethods.DRAWITEMSTRUCT) m.GetLParam(typeof(System.Windows.Forms.NativeMethods.DRAWITEMSTRUCT));
            MenuItem item1 = MenuItem.GetMenuItemFromItemData(drawitemstruct1.itemData);
            if (item1 != null)
            {
                item1.WmDrawItem(ref m);
            }
        }

        private void WmEraseBkgnd(ref Message m)
        {
            if (this.GetStyle(ControlStyles.UserPaint))
            {
                if (!this.GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    IntPtr ptr1 = m.WParam;
                    if (ptr1 == IntPtr.Zero)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
                    System.Windows.Forms.UnsafeNativeMethods.GetClientRect(new HandleRef(this, this.Handle), out rect1);
                    PaintEventArgs args1 = new PaintEventArgs(ptr1, Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom));
                    this.PaintWithErrorHandling(args1, 1, true);
                }
                m.Result = (IntPtr) 1;
            }
            else
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmExitMenuLoop(ref Message m)
        {
            if ((((int) ((long) m.WParam)) != 0) && true)
            {
                System.Windows.Forms.ContextMenu menu1 = (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
                if (menu1 != null)
                {
                    menu1.OnCollapse(EventArgs.Empty);
                }
            }
            this.DefWndProc(ref m);
        }

        private void WmGetControlName(ref Message m)
        {
            string text1;
            if (this.Site != null)
            {
                text1 = this.Site.Name;
            }
            else
            {
                text1 = this.Name;
            }
            if (text1 == null)
            {
                text1 = "";
            }
            this.MarshalStringToMessage(text1, ref m);
        }

        private void WmGetControlType(ref Message m)
        {
            string text1 = base.GetType().AssemblyQualifiedName;
            this.MarshalStringToMessage(text1, ref m);
        }

        private void WmGetObject(ref Message m)
        {
            InternalAccessibleObject obj1 = null;
            AccessibleObject obj2 = this.GetAccessibilityObject((int) ((long) m.LParam));
            if (obj2 != null)
            {
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    obj1 = new InternalAccessibleObject(obj2);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }
            if (obj1 != null)
            {
                Guid guid1 = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
                try
                {
                    object obj3 = obj1;
                    IAccessible accessible1 = obj3 as IAccessible;
                    if (accessible1 != null)
                    {
                        throw new InvalidOperationException(System.Windows.Forms.SR.GetString("ControlAccessibileObjectInvalid"));
                    }
                    System.Windows.Forms.UnsafeNativeMethods.IAccessibleInternal internal1 = obj1;
                    if (internal1 == null)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    IntPtr ptr1 = Marshal.GetIUnknownForObject(internal1);
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        m.Result = System.Windows.Forms.UnsafeNativeMethods.LresultFromObject(ref guid1, m.WParam, new HandleRef(obj2, ptr1));
                        return;
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                        Marshal.Release(ptr1);
                    }
                    return;
                }
                catch (Exception exception1)
                {
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("RichControlLresult"), exception1);
                }
                catch
                {
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("RichControlLresult"));
                }
            }
            this.DefWndProc(ref m);
        }

        private void WmHelp(ref Message m)
        {
            HelpInfo info1 = MessageBox.HelpInfo;
            if (info1 != null)
            {
                switch (info1.Option)
                {
                    case 1:
                    {
                        Help.ShowHelp(this, info1.HelpFilePath);
                        break;
                    }
                    case 2:
                    {
                        Help.ShowHelp(this, info1.HelpFilePath, info1.Keyword);
                        break;
                    }
                    case 3:
                    {
                        Help.ShowHelp(this, info1.HelpFilePath, info1.Navigator);
                        break;
                    }
                    case 4:
                    {
                        Help.ShowHelp(this, info1.HelpFilePath, info1.Navigator, info1.Param);
                        break;
                    }
                }
            }
            System.Windows.Forms.NativeMethods.HELPINFO helpinfo1 = (System.Windows.Forms.NativeMethods.HELPINFO) m.GetLParam(typeof(System.Windows.Forms.NativeMethods.HELPINFO));
            HelpEventArgs args1 = new HelpEventArgs(new Point(helpinfo1.MousePos.x, helpinfo1.MousePos.y));
            this.OnHelpRequested(args1);
            if (!args1.Handled)
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmImeChar(ref Message m)
        {
            if (!this.ProcessKeyEventArgs(ref m))
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmImeKillFocus()
        {
            if (!base.DesignMode && this.ImeIsSupported)
            {
                this.UpdateCachedImeMode(this.Handle);
            }
            if (this.Properties.ContainsInteger(Control.PropCharsToIgnore))
            {
                this.Properties.SetInteger(Control.PropCharsToIgnore, 0);
            }
        }

        private void WmImeNotify(ref Message m)
        {
            int num1 = (int) m.WParam;
            if ((num1 == 6) || (num1 == 8))
            {
                this.UpdateCachedImeMode(this.Handle, true);
                if (this.CanEnableIme)
                {
                    this.UnrestrictedImeMode = this.CachedImeMode;
                }
            }
            this.DefWndProc(ref m);
        }

        internal void WmImeSetFocus()
        {
            if (!base.DesignMode && this.ImeIsSupported)
            {
                if (this.CanEnableIme)
                {
                    this.SetImeMode(this.UnrestrictedImeMode);
                }
                else
                {
                    this.SetImeMode(ImeModeConversion.InputLanguageImeModeDisabled);
                }
            }
        }

        private void WmInitMenuPopup(ref Message m)
        {
            System.Windows.Forms.ContextMenu menu1 = (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
            if ((menu1 == null) || !menu1.ProcessInitMenuPopup(m.WParam))
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmInputLangChange(ref Message m)
        {
            ImeModeConversion.InvalidateInputLanguageTable();
            Form form1 = this.FindFormInternal();
            if (form1 != null)
            {
                InputLanguageChangedEventArgs args1 = InputLanguage.CreateInputLanguageChangedEventArgs(m);
                form1.PerformOnInputLanguageChanged(args1);
            }
            this.DefWndProc(ref m);
            if ((!base.DesignMode && this.Focused) && !this.CanEnableIme)
            {
                this.CurrentImeContextMode = ImeModeConversion.InputLanguageImeModeDisabled;
            }
        }

        private void WmInputLangChangeRequest(ref Message m)
        {
            InputLanguageChangingEventArgs args1 = InputLanguage.CreateInputLanguageChangingEventArgs(m);
            if (this.FindFormInternal() != null)
            {
                this.FindFormInternal().PerformOnInputLanguageChanging(args1);
            }
            if (!args1.Cancel)
            {
                this.DefWndProc(ref m);
            }
            else
            {
                m.Result = IntPtr.Zero;
            }
        }

        private void WmKeyChar(ref Message m)
        {
            if (!this.ProcessKeyMessage(ref m))
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmKillFocus(ref Message m)
        {
            this.WmImeKillFocus();
            this.DefWndProc(ref m);
            this.OnLostFocus(EventArgs.Empty);
        }

        private void WmMeasureItem(ref Message m)
        {
            if (m.WParam == IntPtr.Zero)
            {
                System.Windows.Forms.NativeMethods.MEASUREITEMSTRUCT measureitemstruct1 = (System.Windows.Forms.NativeMethods.MEASUREITEMSTRUCT) m.GetLParam(typeof(System.Windows.Forms.NativeMethods.MEASUREITEMSTRUCT));
                MenuItem item1 = MenuItem.GetMenuItemFromItemData(measureitemstruct1.itemData);
                if (item1 == null)
                {
                    return;
                }
                item1.WmMeasureItem(ref m);
            }
            else
            {
                this.WmOwnerDraw(ref m);
            }
        }

        private void WmMenuChar(ref Message m)
        {
            Menu menu1 = this.ContextMenu;
            if (menu1 != null)
            {
                menu1.WmMenuChar(ref m);
                if (m.Result == IntPtr.Zero)
                {
                }
            }
        }

        private void WmMenuSelect(ref Message m)
        {
            int num1 = System.Windows.Forms.NativeMethods.Util.LOWORD(m.WParam);
            int num2 = System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam);
            IntPtr ptr1 = m.LParam;
            MenuItem item1 = null;
            if ((num2 & 0x2000) == 0)
            {
                if ((num2 & 0x10) == 0)
                {
                    Command command1 = Command.GetCommandFromID(num1);
                    if (command1 != null)
                    {
                        object obj1 = command1.Target;
                        if ((obj1 != null) && (obj1 is MenuItem.MenuItemData))
                        {
                            item1 = ((MenuItem.MenuItemData) obj1).baseItem;
                        }
                    }
                }
                else
                {
                    item1 = this.GetMenuItemFromHandleId(ptr1, num1);
                }
            }
            if (item1 != null)
            {
                item1.PerformSelect();
            }
            this.DefWndProc(ref m);
        }

        private void WmMouseDown(ref Message m, System.Windows.Forms.MouseButtons button, int clicks)
        {
            System.Windows.Forms.MouseButtons buttons1 = Control.MouseButtons;
            this.SetState(0x8000000, true);
            if (!this.GetStyle(ControlStyles.UserMouse))
            {
                this.DefWndProc(ref m);
            }
            else if ((button == System.Windows.Forms.MouseButtons.Left) && this.GetStyle(ControlStyles.Selectable))
            {
                this.FocusInternal();
            }
            if (buttons1 == Control.MouseButtons)
            {
                if (!this.GetState2(0x10))
                {
                    this.CaptureInternal = true;
                }
                if ((buttons1 == Control.MouseButtons) && this.Enabled)
                {
                    this.OnMouseDown(new MouseEventArgs(button, clicks, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
                }
            }
        }

        private void WmMouseEnter(ref Message m)
        {
            this.DefWndProc(ref m);
            this.OnMouseEnter(EventArgs.Empty);
        }

        private void WmMouseHover(ref Message m)
        {
            this.DefWndProc(ref m);
            this.OnMouseHover(EventArgs.Empty);
        }

        private void WmMouseLeave(ref Message m)
        {
            this.DefWndProc(ref m);
            this.OnMouseLeave(EventArgs.Empty);
        }

        private void WmMouseMove(ref Message m)
        {
            if (!this.GetStyle(ControlStyles.UserMouse))
            {
                this.DefWndProc(ref m);
            }
            this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
        }

        private void WmMouseUp(ref Message m, System.Windows.Forms.MouseButtons button, int clicks)
        {
            try
            {
                int num1 = System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam);
                int num2 = System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam);
                Point point1 = new Point(num1, num2);
                point1 = this.PointToScreen(point1);
                if (!this.GetStyle(ControlStyles.UserMouse))
                {
                    this.DefWndProc(ref m);
                }
                else if (button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.SendMessage(0x7b, this.Handle, System.Windows.Forms.NativeMethods.Util.MAKELPARAM(point1.X, point1.Y));
                }
                bool flag1 = false;
                if ((((this.controlStyle & ControlStyles.StandardClick) == ControlStyles.StandardClick) && this.GetState(0x8000000)) && (!this.IsDisposed && (System.Windows.Forms.UnsafeNativeMethods.WindowFromPoint(point1.X, point1.Y) == this.Handle)))
                {
                    flag1 = true;
                }
                if (flag1 && !this.ValidationCancelled)
                {
                    if (!this.GetState(0x4000000))
                    {
                        this.OnClick(new MouseEventArgs(button, clicks, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
                        this.OnMouseClick(new MouseEventArgs(button, clicks, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
                    }
                    else
                    {
                        this.OnDoubleClick(new MouseEventArgs(button, 2, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
                        this.OnMouseDoubleClick(new MouseEventArgs(button, 2, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
                    }
                }
                this.OnMouseUp(new MouseEventArgs(button, clicks, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
            }
            finally
            {
                this.SetState(0x4000000, false);
                this.SetState(0x8000000, false);
                this.SetState(0x10000000, false);
                this.CaptureInternal = false;
            }
        }

        private void WmMouseWheel(ref Message m)
        {
            Point point1 = new Point(System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam));
            point1 = this.PointToClient(point1);
            HandledMouseEventArgs args1 = new HandledMouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, point1.X, point1.Y, System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.WParam));
            this.OnMouseWheel(args1);
            m.Result = args1.Handled ? IntPtr.Zero : ((IntPtr) 1);
            if (!args1.Handled)
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmMove(ref Message m)
        {
            this.DefWndProc(ref m);
            this.UpdateBounds();
        }

        private unsafe void WmNotify(ref Message m)
        {
            System.Windows.Forms.NativeMethods.NMHDR* nmhdrPtr1 = (System.Windows.Forms.NativeMethods.NMHDR*) m.LParam;
            if (!Control.ReflectMessageInternal(nmhdrPtr1->hwndFrom, ref m))
            {
                if (nmhdrPtr1->code == -521)
                {
                    m.Result = System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(null, nmhdrPtr1->hwndFrom), (int) (0x2000 + m.Msg), m.WParam, m.LParam);
                }
                else
                {
                    if (nmhdrPtr1->code == -522)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(null, nmhdrPtr1->hwndFrom), (int) (0x2000 + m.Msg), m.WParam, m.LParam);
                    }
                    this.DefWndProc(ref m);
                }
            }
        }

        private void WmNotifyFormat(ref Message m)
        {
            if (!Control.ReflectMessageInternal(m.WParam, ref m))
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmOwnerDraw(ref Message m)
        {
            bool flag1 = false;
            if (!Control.ReflectMessageInternal(m.WParam, ref m))
            {
                IntPtr ptr1 = this.window.GetHandleFromID((short) System.Windows.Forms.NativeMethods.Util.LOWORD(m.WParam));
                if (ptr1 != IntPtr.Zero)
                {
                    Control control1 = Control.FromHandleInternal(ptr1);
                    if (control1 != null)
                    {
                        m.Result = control1.SendMessage((int) (0x2000 + m.Msg), ptr1, m.LParam);
                        flag1 = true;
                    }
                }
            }
            else
            {
                flag1 = true;
            }
            if (!flag1)
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmPaint(ref Message m)
        {
            if (this.DoubleBuffered || (this.GetStyle(ControlStyles.AllPaintingInWmPaint) && this.DoubleBufferingEnabled))
            {
                IntPtr ptr1;
                Rectangle rectangle1;
                System.Windows.Forms.NativeMethods.PAINTSTRUCT paintstruct1 = new System.Windows.Forms.NativeMethods.PAINTSTRUCT();
                bool flag2 = false;
                if (m.WParam == IntPtr.Zero)
                {
                    ptr1 = System.Windows.Forms.UnsafeNativeMethods.BeginPaint(new HandleRef(this, this.Handle), out paintstruct1);
                    rectangle1 = new Rectangle(paintstruct1.rcPaint_left, paintstruct1.rcPaint_top, paintstruct1.rcPaint_right - paintstruct1.rcPaint_left, paintstruct1.rcPaint_bottom - paintstruct1.rcPaint_top);
                    flag2 = true;
                }
                else
                {
                    ptr1 = m.WParam;
                    rectangle1 = this.ClientRectangle;
                }
                IntPtr ptr2 = Control.SetUpPalette(ptr1, false, false);
                try
                {
                    if ((rectangle1.Width > 0) && (rectangle1.Height > 0))
                    {
                        Rectangle rectangle2 = this.ClientRectangle;
                        using (BufferedGraphics graphics1 = this.BufferContext.Allocate(ptr1, rectangle2))
                        {
                            Graphics graphics2 = graphics1.Graphics;
                            graphics2.SetClip(rectangle1);
                            GraphicsState state1 = graphics2.Save();
                            using (PaintEventArgs args1 = new PaintEventArgs(graphics2, rectangle1))
                            {
                                this.PaintWithErrorHandling(args1, 1, false);
                                graphics2.Restore(state1);
                                this.PaintWithErrorHandling(args1, 2, false);
                                graphics1.Render();
                            }
                        }
                    }
                }
                finally
                {
                    if (ptr2 != IntPtr.Zero)
                    {
                        System.Windows.Forms.SafeNativeMethods.SelectPalette(new HandleRef(null, ptr1), new HandleRef(null, ptr2), 0);
                    }
                }
                if (flag2)
                {
                    System.Windows.Forms.UnsafeNativeMethods.EndPaint(new HandleRef(this, this.Handle), ref paintstruct1);
                }
            }
            else
            {
                if (m.WParam == IntPtr.Zero)
                {
                    System.Windows.Forms.NativeMethods.PAINTSTRUCT paintstruct2 = new System.Windows.Forms.NativeMethods.PAINTSTRUCT();
                    IntPtr ptr3 = this.Handle;
                    IntPtr ptr4 = System.Windows.Forms.UnsafeNativeMethods.BeginPaint(new HandleRef(this, ptr3), out paintstruct2);
                    IntPtr ptr5 = Control.SetUpPalette(ptr4, false, false);
                    try
                    {
                        PaintEventArgs args2 = new PaintEventArgs(ptr4, new Rectangle(paintstruct2.rcPaint_left, paintstruct2.rcPaint_top, paintstruct2.rcPaint_right - paintstruct2.rcPaint_left, paintstruct2.rcPaint_bottom - paintstruct2.rcPaint_top));
                        try
                        {
                            if (this.GetStyle(ControlStyles.AllPaintingInWmPaint))
                            {
                                this.PaintWithErrorHandling(args2, 1, false);
                                args2.ResetGraphics();
                            }
                            this.PaintWithErrorHandling(args2, 2, false);
                            return;
                        }
                        finally
                        {
                            args2.Dispose();
                            if (!this.IsDisposed && this.IsHandleCreated)
                            {
                                ptr3 = this.Handle;
                            }
                            System.Windows.Forms.UnsafeNativeMethods.EndPaint(new HandleRef(this, ptr3), ref paintstruct2);
                        }
                        return;
                    }
                    finally
                    {
                        if (ptr5 != IntPtr.Zero)
                        {
                            System.Windows.Forms.SafeNativeMethods.SelectPalette(new HandleRef(null, ptr4), new HandleRef(null, ptr5), 0);
                        }
                    }
                }
                PaintEventArgs args3 = new PaintEventArgs(m.WParam, this.ClientRectangle);
                this.PaintWithErrorHandling(args3, 2, true);
            }
        }

        private void WmParentNotify(ref Message m)
        {
            int num1 = System.Windows.Forms.NativeMethods.Util.LOWORD(m.WParam);
            IntPtr ptr1 = IntPtr.Zero;
            switch (num1)
            {
                case 1:
                {
                    ptr1 = m.LParam;
                    break;
                }
                case 2:
                {
                    break;
                }
                default:
                {
                    ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetDlgItem(new HandleRef(this, this.Handle), System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam));
                    break;
                }
            }
            if ((ptr1 == IntPtr.Zero) || !Control.ReflectMessageInternal(ptr1, ref m))
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmPrintClient(ref Message m)
        {
            using (PaintEventArgs args1 = new PrintPaintEventArgs(m, m.WParam, this.ClientRectangle))
            {
                this.OnPrint(args1);
            }
        }

        private void WmQueryNewPalette(ref Message m)
        {
            IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetDC(new HandleRef(this, this.Handle));
            try
            {
                Control.SetUpPalette(ptr1, true, true);
            }
            finally
            {
                System.Windows.Forms.UnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.Handle), new HandleRef(null, ptr1));
            }
            this.Invalidate(true);
            m.Result = (IntPtr) 1;
            this.DefWndProc(ref m);
        }

        private void WmSetCursor(ref Message m)
        {
            if ((m.WParam == this.InternalHandle) && (System.Windows.Forms.NativeMethods.Util.LOWORD(m.LParam) == 1))
            {
                System.Windows.Forms.Cursor.CurrentInternal = this.Cursor;
            }
            else
            {
                this.DefWndProc(ref m);
            }
        }

        private void WmSetFocus(ref Message m)
        {
            this.WmImeSetFocus();
            if (!this.HostedInWin32DialogManager)
            {
                IContainerControl control1 = this.GetContainerControlInternal();
                if (control1 != null)
                {
                    bool flag1;
                    ContainerControl control2 = control1 as ContainerControl;
                    if (control2 != null)
                    {
                        flag1 = control2.ActivateControlInternal(this);
                    }
                    else
                    {
                        System.Windows.Forms.IntSecurity.ModifyFocus.Assert();
                        try
                        {
                            flag1 = control1.ActivateControl(this);
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                    }
                    if (!flag1)
                    {
                        return;
                    }
                }
            }
            this.DefWndProc(ref m);
            this.OnGotFocus(EventArgs.Empty);
        }

        private void WmShowWindow(ref Message m)
        {
            this.DefWndProc(ref m);
            if ((this.state & 0x10) != 0)
            {
                return;
            }
            bool flag1 = m.WParam != IntPtr.Zero;
            bool flag2 = this.Visible;
            if (flag1)
            {
                bool flag3 = this.GetState(2);
                this.SetState(2, true);
                bool flag4 = false;
                try
                {
                    this.CreateControl();
                    flag4 = true;
                    goto Label_0081;
                }
                finally
                {
                    if (!flag4)
                    {
                        this.SetState(2, flag3);
                    }
                }
            }
            bool flag5 = this.GetTopLevel();
            if (this.ParentInternal != null)
            {
                flag5 = this.ParentInternal.Visible;
            }
            if (flag5)
            {
                this.SetState(2, false);
            }
        Label_0081:
            if (!this.GetState(0x20000000) && (flag2 != flag1))
            {
                this.OnVisibleChanged(EventArgs.Empty);
            }
        }

        private void WmUpdateUIState(ref Message m)
        {
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = (this.uiCuesState & 240) != 0;
            bool flag4 = (this.uiCuesState & 15) != 0;
            if (flag3)
            {
                flag1 = this.ShowKeyboardCues;
            }
            if (flag4)
            {
                flag2 = this.ShowFocusCues;
            }
            this.DefWndProc(ref m);
            int num1 = System.Windows.Forms.NativeMethods.Util.LOWORD(m.WParam);
            if (num1 != 3)
            {
                UICues cues1 = UICues.None;
                if ((System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam) & 2) != 0)
                {
                    bool flag5 = num1 == 2;
                    if ((flag5 != flag1) || !flag3)
                    {
                        cues1 |= UICues.ChangeKeyboard;
                        this.uiCuesState &= -241;
                        this.uiCuesState |= (flag5 ? 0x20 : 0x10);
                    }
                    if (flag5)
                    {
                        cues1 |= UICues.ShowKeyboard;
                    }
                }
                if ((System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam) & 1) != 0)
                {
                    bool flag6 = num1 == 2;
                    if ((flag6 != flag2) || !flag4)
                    {
                        cues1 |= UICues.ChangeFocus;
                        this.uiCuesState &= -16;
                        this.uiCuesState |= (flag6 ? 2 : 1);
                    }
                    if (flag6)
                    {
                        cues1 |= UICues.ShowFocus;
                    }
                }
                if ((cues1 & UICues.Changed) != UICues.None)
                {
                    this.OnChangeUICues(new UICuesEventArgs(cues1));
                    this.Invalidate(true);
                }
            }
        }

        private unsafe void WmWindowPosChanged(ref Message m)
        {
            this.DefWndProc(ref m);
            this.UpdateBounds();
            if (((this.parent != null) && (System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle)) == this.parent.InternalHandle)) && ((this.state & 0x100) == 0))
            {
                System.Windows.Forms.NativeMethods.WINDOWPOS* windowposPtr1 = (System.Windows.Forms.NativeMethods.WINDOWPOS*) m.LParam;
                if ((windowposPtr1->flags & 4) == 0)
                {
                    this.parent.UpdateChildControlIndex(this);
                }
            }
        }

        private unsafe void WmWindowPosChanging(ref Message m)
        {
            if (this.IsActiveX)
            {
                System.Windows.Forms.NativeMethods.WINDOWPOS* windowposPtr1 = (System.Windows.Forms.NativeMethods.WINDOWPOS*) m.LParam;
                bool flag1 = false;
                if (((windowposPtr1->flags & 2) == 0) && ((windowposPtr1->x != this.Left) || (windowposPtr1->y != this.Top)))
                {
                    flag1 = true;
                }
                if (((windowposPtr1->flags & 1) == 0) && ((windowposPtr1->cx != this.Width) || (windowposPtr1->cy != this.Height)))
                {
                    flag1 = true;
                }
                if (flag1)
                {
                    this.ActiveXUpdateBounds(ref windowposPtr1->x, ref windowposPtr1->y, ref windowposPtr1->cx, ref windowposPtr1->cy, windowposPtr1->flags);
                }
            }
            this.DefWndProc(ref m);
        }

        [SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected virtual void WndProc(ref Message m)
        {
            if ((this.controlStyle & ControlStyles.EnableNotifyMessage) == ControlStyles.EnableNotifyMessage)
            {
                this.OnNotifyMessage(m);
            }
            int num1 = m.Msg;
            if (num1 <= 0x105)
            {
                if (num1 <= 0x2f)
                {
                    if (num1 <= 20)
                    {
                        switch (num1)
                        {
                            case 1:
                            {
                                this.WmCreate(ref m);
                                return;
                            }
                            case 2:
                            {
                                this.WmDestroy(ref m);
                                return;
                            }
                            case 3:
                            {
                                this.WmMove(ref m);
                                return;
                            }
                            case 4:
                            case 5:
                            case 6:
                            {
                                goto Label_060E;
                            }
                            case 7:
                            {
                                this.WmSetFocus(ref m);
                                return;
                            }
                            case 8:
                            {
                                this.WmKillFocus(ref m);
                                return;
                            }
                            case 15:
                            {
                                if (!this.GetStyle(ControlStyles.UserPaint))
                                {
                                    this.DefWndProc(ref m);
                                    return;
                                }
                                this.WmPaint(ref m);
                                return;
                            }
                            case 0x10:
                            {
                                this.WmClose(ref m);
                                return;
                            }
                            case 20:
                            {
                                this.WmEraseBkgnd(ref m);
                                return;
                            }
                        }
                        goto Label_060E;
                    }
                    switch (num1)
                    {
                        case 0x18:
                        {
                            this.WmShowWindow(ref m);
                            return;
                        }
                        case 0x19:
                        {
                            goto Label_0419;
                        }
                        case 0x20:
                        {
                            this.WmSetCursor(ref m);
                            return;
                        }
                        case 0x2b:
                        {
                            this.WmDrawItem(ref m);
                            return;
                        }
                        case 0x2c:
                        {
                            this.WmMeasureItem(ref m);
                            return;
                        }
                        case 0x2d:
                        case 0x2e:
                        case 0x2f:
                        {
                            goto Label_0421;
                        }
                    }
                    goto Label_060E;
                }
                if (num1 <= 0x47)
                {
                    if (num1 == 0x39)
                    {
                        goto Label_0421;
                    }
                    if (num1 == 0x3d)
                    {
                        this.WmGetObject(ref m);
                        return;
                    }
                    switch (num1)
                    {
                        case 70:
                        {
                            this.WmWindowPosChanging(ref m);
                            return;
                        }
                        case 0x47:
                        {
                            this.WmWindowPosChanged(ref m);
                            return;
                        }
                    }
                    goto Label_060E;
                }
                if (num1 <= 0x7b)
                {
                    switch (num1)
                    {
                        case 0x4e:
                        {
                            this.WmNotify(ref m);
                            return;
                        }
                        case 0x4f:
                        case 0x52:
                        case 0x54:
                        {
                            goto Label_060E;
                        }
                        case 80:
                        {
                            this.WmInputLangChangeRequest(ref m);
                            return;
                        }
                        case 0x51:
                        {
                            this.WmInputLangChange(ref m);
                            return;
                        }
                        case 0x53:
                        {
                            this.WmHelp(ref m);
                            return;
                        }
                        case 0x55:
                        {
                            this.WmNotifyFormat(ref m);
                            return;
                        }
                        case 0x7b:
                        {
                            this.WmContextMenu(ref m);
                            return;
                        }
                    }
                    goto Label_060E;
                }
                if (num1 == 0x7e)
                {
                    this.WmDisplayChange(ref m);
                    return;
                }
                switch (num1)
                {
                    case 0x100:
                    case 0x101:
                    case 0x102:
                    case 260:
                    case 0x105:
                    {
                        this.WmKeyChar(ref m);
                        return;
                    }
                    case 0x103:
                    {
                        goto Label_060E;
                    }
                }
                goto Label_060E;
            }
            if (num1 <= 0x282)
            {
                if (num1 <= 0x128)
                {
                    switch (num1)
                    {
                        case 0x111:
                        {
                            this.WmCommand(ref m);
                            return;
                        }
                        case 0x112:
                        {
                            if (((((int) m.WParam) & 0xfff0) != 0xf100) || !ToolStripManager.ProcessMenuKey(ref m))
                            {
                                this.DefWndProc(ref m);
                                return;
                            }
                            m.Result = IntPtr.Zero;
                            return;
                        }
                        case 0x113:
                        case 0x116:
                        {
                            goto Label_060E;
                        }
                        case 0x114:
                        case 0x115:
                        {
                            goto Label_0421;
                        }
                        case 0x117:
                        {
                            this.WmInitMenuPopup(ref m);
                            return;
                        }
                        case 0x11f:
                        {
                            this.WmMenuSelect(ref m);
                            return;
                        }
                        case 0x120:
                        {
                            this.WmMenuChar(ref m);
                            return;
                        }
                        case 0x128:
                        {
                            this.WmUpdateUIState(ref m);
                            return;
                        }
                    }
                    goto Label_060E;
                }
                switch (num1)
                {
                    case 0x132:
                    case 0x133:
                    case 0x134:
                    case 0x135:
                    case 310:
                    case 0x137:
                    case 0x138:
                    {
                        goto Label_0419;
                    }
                    case 0x200:
                    {
                        this.WmMouseMove(ref m);
                        return;
                    }
                    case 0x201:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Left, 1);
                        return;
                    }
                    case 0x202:
                    {
                        this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Left, 1);
                        return;
                    }
                    case 0x203:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Left, 2);
                        if (this.GetStyle(ControlStyles.StandardDoubleClick))
                        {
                            this.SetState(0x4000000, true);
                        }
                        return;
                    }
                    case 0x204:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Right, 1);
                        return;
                    }
                    case 0x205:
                    {
                        this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Right, 1);
                        return;
                    }
                    case 0x206:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Right, 2);
                        if (this.GetStyle(ControlStyles.StandardDoubleClick))
                        {
                            this.SetState(0x4000000, true);
                        }
                        return;
                    }
                    case 0x207:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Middle, 1);
                        return;
                    }
                    case 520:
                    {
                        this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Middle, 1);
                        return;
                    }
                    case 0x209:
                    {
                        this.WmMouseDown(ref m, System.Windows.Forms.MouseButtons.Middle, 2);
                        if (this.GetStyle(ControlStyles.StandardDoubleClick))
                        {
                            this.SetState(0x4000000, true);
                        }
                        return;
                    }
                    case 0x20a:
                    {
                        this.WmMouseWheel(ref m);
                        return;
                    }
                    case 0x20b:
                    {
                        this.WmMouseDown(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 1);
                        return;
                    }
                    case 0x20c:
                    {
                        this.WmMouseUp(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 1);
                        return;
                    }
                    case 0x20d:
                    {
                        this.WmMouseDown(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 2);
                        if (this.GetStyle(ControlStyles.StandardDoubleClick))
                        {
                            this.SetState(0x4000000, true);
                        }
                        return;
                    }
                    case 0x20e:
                    case 0x20f:
                    case 0x211:
                    case 0x213:
                    case 0x214:
                    {
                        goto Label_060E;
                    }
                    case 0x210:
                    {
                        this.WmParentNotify(ref m);
                        return;
                    }
                    case 530:
                    {
                        this.WmExitMenuLoop(ref m);
                        return;
                    }
                    case 0x215:
                    {
                        this.WmCaptureChanged(ref m);
                        return;
                    }
                    case 0x282:
                    {
                        this.WmImeNotify(ref m);
                        return;
                    }
                }
                goto Label_060E;
            }
            if (num1 <= 0x30f)
            {
                if (num1 == 0x286)
                {
                    this.WmImeChar(ref m);
                    return;
                }
                switch (num1)
                {
                    case 0x2a1:
                    {
                        this.WmMouseHover(ref m);
                        return;
                    }
                    case 0x2a2:
                    {
                        goto Label_060E;
                    }
                    case 0x2a3:
                    {
                        this.WmMouseLeave(ref m);
                        return;
                    }
                    case 0x30f:
                    {
                        this.WmQueryNewPalette(ref m);
                        return;
                    }
                }
                goto Label_060E;
            }
            if (num1 <= 0x2019)
            {
                if (num1 == 0x318)
                {
                    if (this.GetStyle(ControlStyles.UserPaint))
                    {
                        this.WmPrintClient(ref m);
                        return;
                    }
                    this.DefWndProc(ref m);
                    return;
                }
                if (num1 == 0x2019)
                {
                    goto Label_0419;
                }
                goto Label_060E;
            }
            if (num1 == 0x2055)
            {
                m.Result = (Marshal.SystemDefaultCharSize == 1) ? ((IntPtr) 1) : ((IntPtr) 2);
                return;
            }
            switch (num1)
            {
                case 0x2132:
                case 0x2133:
                case 0x2134:
                case 0x2135:
                case 0x2136:
                case 0x2137:
                case 0x2138:
                {
                    goto Label_0419;
                }
                default:
                {
                    goto Label_060E;
                }
            }
        Label_0419:
            this.WmCtlColorControl(ref m);
            return;
        Label_0421:
            if (!Control.ReflectMessageInternal(m.LParam, ref m))
            {
                this.DefWndProc(ref m);
            }
            return;
        Label_060E:
            if ((m.Msg == Control.threadCallbackMessage) && (m.Msg != 0))
            {
                this.InvokeMarshaledCallbacks();
            }
            else if (m.Msg == Control.WM_GETCONTROLNAME)
            {
                this.WmGetControlName(ref m);
            }
            else if (m.Msg == Control.WM_GETCONTROLTYPE)
            {
                this.WmGetControlType(ref m);
            }
            else
            {
                if (Control.mouseWheelRoutingNeeded && (m.Msg == Control.mouseWheelMessage))
                {
                    Keys keys1 = Keys.None;
                    keys1 |= ((System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x11) < 0) ? Keys.Back : Keys.None);
                    keys1 |= ((System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x10) < 0) ? Keys.MButton : Keys.None);
                    IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetFocus();
                    if (ptr1 == IntPtr.Zero)
                    {
                        this.SendMessage(m.Msg, (IntPtr) (((Keys) (((int) ((long) m.WParam)) << 0x10)) | keys1), m.LParam);
                    }
                    else
                    {
                        IntPtr ptr2 = IntPtr.Zero;
                        IntPtr ptr3 = System.Windows.Forms.UnsafeNativeMethods.GetDesktopWindow();
                        while (((ptr2 == IntPtr.Zero) && (ptr1 != IntPtr.Zero)) && (ptr1 != ptr3))
                        {
                            ptr2 = System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(null, ptr1), 0x20a, (int) (((Keys) (((int) ((long) m.WParam)) << 0x10)) | keys1), m.LParam);
                            ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(null, ptr1));
                        }
                    }
                }
                if (m.Msg == System.Windows.Forms.NativeMethods.WM_MOUSEENTER)
                {
                    this.WmMouseEnter(ref m);
                }
                else
                {
                    this.DefWndProc(ref m);
                }
            }
        }

        private void WndProcException(Exception e)
        {
            Application.OnThreadException(e);
        }


        // Properties
        [System.Windows.Forms.SRDescription("ControlAccessibilityObjectDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AccessibleObject AccessibilityObject
        {
            get
            {
                AccessibleObject obj1 = (AccessibleObject) this.Properties.GetObject(Control.PropAccessibility);
                if (obj1 == null)
                {
                    obj1 = this.CreateAccessibilityInstance();
                    if (!(obj1 is ControlAccessibleObject))
                    {
                        return null;
                    }
                    this.Properties.SetObject(Control.PropAccessibility, obj1);
                }
                return obj1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlAccessibleDefaultActionDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatAccessibility")]
        public string AccessibleDefaultActionDescription
        {
            get
            {
                return (string) this.Properties.GetObject(Control.PropAccessibleDefaultActionDescription);
            }
            set
            {
                this.Properties.SetObject(Control.PropAccessibleDefaultActionDescription, value);
            }
        }

        [System.Windows.Forms.SRDescription("ControlAccessibleDescriptionDescr"), Localizable(true), System.Windows.Forms.SRCategory("CatAccessibility"), DefaultValue((string) null)]
        public string AccessibleDescription
        {
            get
            {
                return (string) this.Properties.GetObject(Control.PropAccessibleDescription);
            }
            set
            {
                this.Properties.SetObject(Control.PropAccessibleDescription, value);
            }
        }

        [System.Windows.Forms.SRDescription("ControlAccessibleNameDescr"), Localizable(true), DefaultValue((string) null), System.Windows.Forms.SRCategory("CatAccessibility")]
        public string AccessibleName
        {
            get
            {
                return (string) this.Properties.GetObject(Control.PropAccessibleName);
            }
            set
            {
                this.Properties.SetObject(Control.PropAccessibleName, value);
            }
        }

        [System.Windows.Forms.SRCategory("CatAccessibility"), DefaultValue(-1), System.Windows.Forms.SRDescription("ControlAccessibleRoleDescr")]
        public System.Windows.Forms.AccessibleRole AccessibleRole
        {
            get
            {
                bool flag1;
                int num1 = this.Properties.GetInteger(Control.PropAccessibleRole, out flag1);
                if (flag1)
                {
                    return (System.Windows.Forms.AccessibleRole) num1;
                }
                return System.Windows.Forms.AccessibleRole.Default;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, System.Windows.Forms.AccessibleRole.Default, System.Windows.Forms.AccessibleRole.OutlineButton))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(System.Windows.Forms.AccessibleRole));
                }
                this.Properties.SetInteger(Control.PropAccessibleRole, (int) value);
            }
        }

        private System.Drawing.Color ActiveXAmbientBackColor
        {
            get
            {
                return this.ActiveXInstance.AmbientBackColor;
            }
        }

        private System.Drawing.Font ActiveXAmbientFont
        {
            get
            {
                return this.ActiveXInstance.AmbientFont;
            }
        }

        private System.Drawing.Color ActiveXAmbientForeColor
        {
            get
            {
                return this.ActiveXInstance.AmbientForeColor;
            }
        }

        private bool ActiveXEventsFrozen
        {
            get
            {
                return this.ActiveXInstance.EventsFrozen;
            }
        }

        private IntPtr ActiveXHWNDParent
        {
            get
            {
                return this.ActiveXInstance.HWNDParent;
            }
        }

        private ActiveXImpl ActiveXInstance
        {
            get
            {
                ActiveXImpl impl1 = (ActiveXImpl) this.Properties.GetObject(Control.PropActiveXImpl);
                if (impl1 == null)
                {
                    if (this.GetState(0x80000))
                    {
                        throw new NotSupportedException(System.Windows.Forms.SR.GetString("AXTopLevelSource"));
                    }
                    impl1 = new ActiveXImpl(this);
                    this.Properties.SetObject(Control.PropActiveXImpl, impl1);
                }
                return impl1;
            }
        }

        [DefaultValue(false), System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlAllowDropDescr")]
        public virtual bool AllowDrop
        {
            get
            {
                return this.GetState(0x40);
            }
            set
            {
                if (this.GetState(0x40) != value)
                {
                    if (value && !this.IsHandleCreated)
                    {
                        System.Windows.Forms.IntSecurity.ClipboardRead.Demand();
                    }
                    this.SetState(0x40, value);
                    if (this.IsHandleCreated)
                    {
                        try
                        {
                            this.SetAcceptDrops(value);
                        }
                        catch
                        {
                            this.SetState(0x40, !value);
                            throw;
                        }
                    }
                }
            }
        }

        private AmbientProperties AmbientPropertiesService
        {
            get
            {
                bool flag1;
                AmbientProperties properties1 = (AmbientProperties) this.Properties.GetObject(Control.PropAmbientPropertiesService, out flag1);
                if (!flag1)
                {
                    if (this.Site != null)
                    {
                        properties1 = (AmbientProperties) this.Site.GetService(typeof(AmbientProperties));
                    }
                    else
                    {
                        properties1 = (AmbientProperties) this.GetService(typeof(AmbientProperties));
                    }
                    if (properties1 != null)
                    {
                        this.Properties.SetObject(Control.PropAmbientPropertiesService, properties1);
                    }
                }
                return properties1;
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), RefreshProperties(RefreshProperties.Repaint), Localizable(true), DefaultValue(5), System.Windows.Forms.SRDescription("ControlAnchorDescr")]
        public virtual AnchorStyles Anchor
        {
            get
            {
                return DefaultLayout.GetAnchor(this);
            }
            set
            {
                DefaultLayout.SetAnchor(this.ParentInternal, this, value);
            }
        }

        [DefaultValue(typeof(Point), "0, 0"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual Point AutoScrollOffset
        {
            get
            {
                if (this.Properties.ContainsObject(Control.PropAutoScrollOffset))
                {
                    return (Point) this.Properties.GetObject(Control.PropAutoScrollOffset);
                }
                return Point.Empty;
            }
            set
            {
                if (this.AutoScrollOffset != value)
                {
                    this.Properties.SetObject(Control.PropAutoScrollOffset, value);
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), System.Windows.Forms.SRDescription("ControlAutoSizeDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All), DefaultValue(false), System.Windows.Forms.SRCategory("CatLayout"), Localizable(true)]
        public virtual bool AutoSize
        {
            get
            {
                return CommonProperties.GetAutoSize(this);
            }
            set
            {
                if (value != this.AutoSize)
                {
                    CommonProperties.SetAutoSize(this, value);
                    if (this.ParentInternal != null)
                    {
                        if (value && (this.ParentInternal.LayoutEngine == DefaultLayout.Instance))
                        {
                            this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
                        }
                        LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
                    }
                    this.OnAutoSizeChanged(EventArgs.Empty);
                }
            }
        }

        [DispId(-501), System.Windows.Forms.SRDescription("ControlBackColorDescr"), System.Windows.Forms.SRCategory("CatAppearance")]
        public virtual System.Drawing.Color BackColor
        {
            get
            {
                System.Drawing.Color color1 = this.RawBackColor;
                if (!color1.IsEmpty)
                {
                    return color1;
                }
                Control control1 = this.ParentInternal;
                if ((control1 != null) && control1.CanAccessProperties)
                {
                    color1 = control1.BackColor;
                    if (this.IsValidBackColor(color1))
                    {
                        return color1;
                    }
                }
                if (this.IsActiveX)
                {
                    color1 = this.ActiveXAmbientBackColor;
                }
                if (color1.IsEmpty)
                {
                    AmbientProperties properties1 = this.AmbientPropertiesService;
                    if (properties1 != null)
                    {
                        color1 = properties1.BackColor;
                    }
                }
                if (!color1.IsEmpty && this.IsValidBackColor(color1))
                {
                    return color1;
                }
                return Control.DefaultBackColor;
            }
            set
            {
                if ((!value.Equals(System.Drawing.Color.Empty) && !this.GetStyle(ControlStyles.SupportsTransparentBackColor)) && (value.A < 0xff))
                {
                    throw new ArgumentException(System.Windows.Forms.SR.GetString("TransparentBackColorNotAllowed"));
                }
                System.Drawing.Color color1 = this.BackColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropBackColor))
                {
                    this.Properties.SetColor(Control.PropBackColor, value);
                }
                if (!color1.Equals(this.BackColor))
                {
                    this.OnBackColorChanged(EventArgs.Empty);
                }
            }
        }

        internal IntPtr BackColorBrush
        {
            get
            {
                IntPtr ptr1;
                object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                if (obj1 != null)
                {
                    return (IntPtr) obj1;
                }
                if ((!this.Properties.ContainsObject(Control.PropBackColor) && (this.parent != null)) && (this.parent.BackColor == this.BackColor))
                {
                    return this.parent.BackColorBrush;
                }
                System.Drawing.Color color1 = this.BackColor;
                if (ColorTranslator.ToOle(color1) < 0)
                {
                    ptr1 = System.Windows.Forms.SafeNativeMethods.GetSysColorBrush(ColorTranslator.ToOle(color1) & 0xff);
                    this.SetState(0x200000, false);
                }
                else
                {
                    ptr1 = System.Windows.Forms.SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(color1));
                    this.SetState(0x200000, true);
                }
                this.Properties.SetObject(Control.PropBackBrush, ptr1);
                return ptr1;
            }
        }

        [System.Windows.Forms.SRCategory("CatAppearance"), System.Windows.Forms.SRDescription("ControlBackgroundImageDescr"), DefaultValue((string) null), Localizable(true)]
        public virtual Image BackgroundImage
        {
            get
            {
                return (Image) this.Properties.GetObject(Control.PropBackgroundImage);
            }
            set
            {
                if (this.BackgroundImage != value)
                {
                    this.Properties.SetObject(Control.PropBackgroundImage, value);
                    this.OnBackgroundImageChanged(EventArgs.Empty);
                }
            }
        }

        [Localizable(true), System.Windows.Forms.SRDescription("ControlBackgroundImageLayoutDescr"), DefaultValue(1), System.Windows.Forms.SRCategory("CatAppearance")]
        public virtual ImageLayout BackgroundImageLayout
        {
            get
            {
                if (!this.Properties.ContainsObject(Control.PropBackgroundImageLayout))
                {
                    return ImageLayout.Tile;
                }
                return (ImageLayout) this.Properties.GetObject(Control.PropBackgroundImageLayout);
            }
            set
            {
                if (this.BackgroundImageLayout != value)
                {
                    if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, ImageLayout.None, ImageLayout.Zoom))
                    {
                        throw new InvalidEnumArgumentException("value", (int) value, typeof(ImageLayout));
                    }
                    if (((value == ImageLayout.Center) || (value == ImageLayout.Zoom)) || (value == ImageLayout.Stretch))
                    {
                        this.SetStyle(ControlStyles.ResizeRedraw, true);
                        if (ControlPaint.IsImageTransparent(this.BackgroundImage))
                        {
                            this.DoubleBuffered = true;
                        }
                    }
                    this.Properties.SetObject(Control.PropBackgroundImageLayout, value);
                    this.OnBackgroundImageLayoutChanged(EventArgs.Empty);
                }
            }
        }

        internal bool BecomingActiveControl
        {
            get
            {
                return this.GetState2(0x20);
            }
            set
            {
                if (value != this.BecomingActiveControl)
                {
                    this.SetState2(0x20, value);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlBindingContextDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual System.Windows.Forms.BindingContext BindingContext
        {
            get
            {
                return this.BindingContextInternal;
            }
            set
            {
                this.BindingContextInternal = value;
            }
        }

        internal System.Windows.Forms.BindingContext BindingContextInternal
        {
            get
            {
                System.Windows.Forms.BindingContext context1 = (System.Windows.Forms.BindingContext) this.Properties.GetObject(Control.PropBindingManager);
                if (context1 != null)
                {
                    return context1;
                }
                Control control1 = this.ParentInternal;
                if ((control1 != null) && control1.CanAccessProperties)
                {
                    return control1.BindingContext;
                }
                return null;
            }
            set
            {
                System.Windows.Forms.BindingContext context1 = (System.Windows.Forms.BindingContext) this.Properties.GetObject(Control.PropBindingManager);
                System.Windows.Forms.BindingContext context2 = value;
                if (context1 != context2)
                {
                    this.Properties.SetObject(Control.PropBindingManager, context2);
                    this.OnBindingContextChanged(EventArgs.Empty);
                }
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRDescription("ControlBottomDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Bottom
        {
            get
            {
                return (this.y + this.height);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatLayout"), Browsable(false), System.Windows.Forms.SRDescription("ControlBoundsDescr"), EditorBrowsable(EditorBrowsableState.Advanced)]
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(this.x, this.y, this.width, this.height);
            }
            set
            {
                this.SetBounds(value.X, value.Y, value.Width, value.Height, BoundsSpecified.All);
            }
        }

        private BufferedGraphicsContext BufferContext
        {
            get
            {
                return BufferedGraphicsManager.Current;
            }
        }

        internal System.Windows.Forms.ImeMode CachedImeMode
        {
            get
            {
                bool flag1;
                System.Windows.Forms.ImeMode mode1 = (System.Windows.Forms.ImeMode) this.Properties.GetInteger(Control.PropImeMode, out flag1);
                if (!flag1)
                {
                    mode1 = this.DefaultImeMode;
                }
                if (mode1 != System.Windows.Forms.ImeMode.Inherit)
                {
                    return mode1;
                }
                Control control1 = this.ParentInternal;
                if (control1 != null)
                {
                    return control1.CachedImeMode;
                }
                return System.Windows.Forms.ImeMode.NoControl;
            }
        }

        internal bool CacheTextInternal
        {
            get
            {
                bool flag1;
                int num1 = this.Properties.GetInteger(Control.PropCacheTextCount, out flag1);
                if (num1 <= 0)
                {
                    return this.GetStyle(ControlStyles.CacheText);
                }
                return true;
            }
            set
            {
                if (!this.GetStyle(ControlStyles.CacheText) && this.IsHandleCreated)
                {
                    bool flag1;
                    int num1 = this.Properties.GetInteger(Control.PropCacheTextCount, out flag1);
                    if (value)
                    {
                        if (num1 == 0)
                        {
                            this.Properties.SetObject(Control.PropCacheTextField, this.text);
                            if (this.text == null)
                            {
                                this.text = this.WindowText;
                            }
                        }
                        num1++;
                    }
                    else
                    {
                        num1--;
                        if (num1 == 0)
                        {
                            this.text = (string) this.Properties.GetObject(Control.PropCacheTextField, out flag1);
                        }
                    }
                    this.Properties.SetInteger(Control.PropCacheTextCount, num1);
                }
            }
        }

        internal virtual bool CanAccessProperties
        {
            get
            {
                return true;
            }
        }

        internal virtual bool CanEnableIme
        {
            get
            {
                if (this.DefaultImeMode != System.Windows.Forms.ImeMode.NoControl)
                {
                    return (this.DefaultImeMode != System.Windows.Forms.ImeMode.Disable);
                }
                return false;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), System.Windows.Forms.SRCategory("CatFocus"), System.Windows.Forms.SRDescription("ControlCanFocusDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanFocus
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    bool flag1 = System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(this.window, this.Handle));
                    bool flag2 = System.Windows.Forms.SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle));
                    if (flag1)
                    {
                        return flag2;
                    }
                }
                return false;
            }
        }

        protected override bool CanRaiseEvents
        {
            get
            {
                if (this.IsActiveX)
                {
                    return !this.ActiveXEventsFrozen;
                }
                return true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatFocus"), System.Windows.Forms.SRDescription("ControlCanSelectDescr"), Browsable(false)]
        public bool CanSelect
        {
            get
            {
                return this.CanSelectCore();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRDescription("ControlCaptureDescr"), System.Windows.Forms.SRCategory("CatFocus"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Capture
        {
            get
            {
                return this.CaptureInternal;
            }
            set
            {
                if (value)
                {
                    System.Windows.Forms.IntSecurity.GetCapture.Demand();
                }
                this.CaptureInternal = value;
            }
        }

        internal bool CaptureInternal
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    return (System.Windows.Forms.UnsafeNativeMethods.GetCapture() == this.Handle);
                }
                return false;
            }
            set
            {
                if (this.CaptureInternal != value)
                {
                    if (value)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.SetCapture(new HandleRef(this, this.Handle));
                    }
                    else
                    {
                        System.Windows.Forms.SafeNativeMethods.ReleaseCapture();
                    }
                }
            }
        }

        [System.Windows.Forms.SRCategory("CatFocus"), System.Windows.Forms.SRDescription("ControlCausesValidationDescr"), DefaultValue(true)]
        public bool CausesValidation
        {
            get
            {
                return this.GetState(0x20000);
            }
            set
            {
                if (value != this.CausesValidation)
                {
                    this.SetState(0x20000, value);
                    this.OnCausesValidationChanged(EventArgs.Empty);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlCheckForIllegalCrossThreadCalls"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public static bool CheckForIllegalCrossThreadCalls
        {
            get
            {
                return Control.checkForIllegalCrossThreadCalls;
            }
            set
            {
                Control.checkForIllegalCrossThreadCalls = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRDescription("ControlClientRectangleDescr"), System.Windows.Forms.SRCategory("CatLayout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlClientSizeDescr")]
        public System.Drawing.Size ClientSize
        {
            get
            {
                return new System.Drawing.Size(this.clientWidth, this.clientHeight);
            }
            set
            {
                this.SetClientSizeCore(value.Width, value.Height);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ControlCompanyNameDescr"), EditorBrowsable(EditorBrowsableState.Advanced)]
        public string CompanyName
        {
            get
            {
                return this.VersionInfo.CompanyName;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), System.Windows.Forms.SRDescription("ControlContainsFocusDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ContainsFocus
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetFocus();
                    if (ptr1 == IntPtr.Zero)
                    {
                        return false;
                    }
                    if (ptr1 == this.Handle)
                    {
                        return true;
                    }
                    if (System.Windows.Forms.UnsafeNativeMethods.IsChild(new HandleRef(this, this.Handle), new HandleRef(this, ptr1)))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [System.Windows.Forms.SRDescription("ControlContextMenuDescr"), System.Windows.Forms.SRCategory("CatBehavior"), DefaultValue((string) null), Browsable(false)]
        public virtual System.Windows.Forms.ContextMenu ContextMenu
        {
            get
            {
                return (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
            }
            set
            {
                System.Windows.Forms.ContextMenu menu1 = (System.Windows.Forms.ContextMenu) this.Properties.GetObject(Control.PropContextMenu);
                if (menu1 != value)
                {
                    EventHandler handler1 = new EventHandler(this.DetachContextMenu);
                    if (menu1 != null)
                    {
                        menu1.Disposed -= handler1;
                    }
                    this.Properties.SetObject(Control.PropContextMenu, value);
                    if (value != null)
                    {
                        value.Disposed += handler1;
                    }
                    this.OnContextMenuChanged(EventArgs.Empty);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlContextMenuDescr"), System.Windows.Forms.SRCategory("CatBehavior"), DefaultValue((string) null)]
        public virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return (System.Windows.Forms.ContextMenuStrip) this.Properties.GetObject(Control.PropContextMenuStrip);
            }
            set
            {
                System.Windows.Forms.ContextMenuStrip strip1 = this.Properties.GetObject(Control.PropContextMenuStrip) as System.Windows.Forms.ContextMenuStrip;
                if (strip1 != value)
                {
                    EventHandler handler1 = new EventHandler(this.DetachContextMenuStrip);
                    if (strip1 != null)
                    {
                        strip1.Disposed -= handler1;
                    }
                    this.Properties.SetObject(Control.PropContextMenuStrip, value);
                    if (value != null)
                    {
                        value.Disposed += handler1;
                    }
                    this.OnContextMenuStripChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false), System.Windows.Forms.SRDescription("ControlControlsDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 == null)
                {
                    collection1 = this.CreateControlsInstance();
                    this.Properties.SetObject(Control.PropControlsCollection, collection1);
                }
                return collection1;
            }
        }

        [System.Windows.Forms.SRDescription("ControlCreatedDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public bool Created
        {
            get
            {
                return ((this.state & 1) != 0);
            }
        }

        protected virtual System.Windows.Forms.CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                if (this.createParams == null)
                {
                    this.createParams = new System.Windows.Forms.CreateParams();
                }
                System.Windows.Forms.CreateParams params1 = this.createParams;
                params1.Style = 0;
                params1.ExStyle = 0;
                params1.ClassStyle = 0;
                params1.Caption = this.text;
                params1.X = this.x;
                params1.Y = this.y;
                params1.Width = this.width;
                params1.Height = this.height;
                params1.Style = 0x2000000;
                if (this.GetStyle(ControlStyles.ContainerControl))
                {
                    params1.ExStyle |= 0x10000;
                }
                params1.ClassStyle = 8;
                if ((this.state & 0x80000) == 0)
                {
                    params1.Parent = (this.parent == null) ? IntPtr.Zero : this.parent.InternalHandle;
                    params1.Style |= 0x44000000;
                }
                else
                {
                    params1.Parent = IntPtr.Zero;
                }
                if ((this.state & 8) != 0)
                {
                    params1.Style |= 0x10000;
                }
                if ((this.state & 2) != 0)
                {
                    params1.Style |= 0x10000000;
                }
                if (!this.Enabled)
                {
                    params1.Style |= 0x8000000;
                }
                if ((params1.Parent == IntPtr.Zero) && this.IsActiveX)
                {
                    params1.Parent = this.ActiveXHWNDParent;
                }
                if (this.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                {
                    params1.ExStyle |= 0x2000;
                    params1.ExStyle |= 0x1000;
                    params1.ExStyle |= 0x4000;
                }
                return params1;
            }
        }

        internal int CreateThreadId
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    int num1;
                    return System.Windows.Forms.SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num1);
                }
                return System.Windows.Forms.SafeNativeMethods.GetCurrentThreadId();
            }
        }

        internal System.Windows.Forms.ImeMode CurrentImeContextMode
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    return ImeContext.GetImeMode(this.Handle);
                }
                return this.CachedImeMode;
            }
            set
            {
                if (((value == ImeModeConversion.InputLanguageImeModeDisabled) || this.CanEnableIme) && this.IsHandleCreated)
                {
                    ImeContext.SetImeStatus(value, this.Handle);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlCursorDescr"), AmbientValue((string) null), System.Windows.Forms.SRCategory("CatAppearance")]
        public virtual System.Windows.Forms.Cursor Cursor
        {
            get
            {
                if (this.GetState(0x400))
                {
                    return Cursors.WaitCursor;
                }
                System.Windows.Forms.Cursor cursor1 = (System.Windows.Forms.Cursor) this.Properties.GetObject(Control.PropCursor);
                if (cursor1 != null)
                {
                    return cursor1;
                }
                System.Windows.Forms.Cursor cursor2 = this.DefaultCursor;
                if (cursor2 == Cursors.Default)
                {
                    Control control1 = this.ParentInternal;
                    if (control1 != null)
                    {
                        return control1.Cursor;
                    }
                    AmbientProperties properties1 = this.AmbientPropertiesService;
                    if ((properties1 != null) && (properties1.Cursor != null))
                    {
                        return properties1.Cursor;
                    }
                }
                return cursor2;
            }
            set
            {
                System.Windows.Forms.Cursor cursor1 = (System.Windows.Forms.Cursor) this.Properties.GetObject(Control.PropCursor);
                System.Windows.Forms.Cursor cursor2 = this.Cursor;
                if (cursor1 != value)
                {
                    System.Windows.Forms.IntSecurity.ModifyCursor.Demand();
                    this.Properties.SetObject(Control.PropCursor, value);
                }
                if (this.IsHandleCreated)
                {
                    System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT();
                    System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
                    System.Windows.Forms.UnsafeNativeMethods.GetCursorPos(point1);
                    System.Windows.Forms.UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), out rect1);
                    if ((((rect1.left <= point1.x) && (point1.x < rect1.right)) && ((rect1.top <= point1.y) && (point1.y < rect1.bottom))) || (System.Windows.Forms.UnsafeNativeMethods.GetCapture() == this.Handle))
                    {
                        this.SendMessage(0x20, this.Handle, (IntPtr) 1);
                    }
                }
                if (!cursor2.Equals(value))
                {
                    this.OnCursorChanged(EventArgs.Empty);
                }
            }
        }

        [ParenthesizePropertyName(true), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), System.Windows.Forms.SRCategory("CatData"), System.Windows.Forms.SRDescription("ControlBindingsDescr")]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                ControlBindingsCollection collection1 = (ControlBindingsCollection) this.Properties.GetObject(Control.PropBindings);
                if (collection1 == null)
                {
                    collection1 = new ControlBindingsCollection(this);
                    this.Properties.SetObject(Control.PropBindings, collection1);
                }
                return collection1;
            }
        }

        public static System.Drawing.Color DefaultBackColor
        {
            get
            {
                return SystemColors.Control;
            }
        }

        protected virtual System.Windows.Forms.Cursor DefaultCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        public static System.Drawing.Font DefaultFont
        {
            get
            {
                if (Control.defaultFont == null)
                {
                    Control.defaultFont = SystemFonts.DefaultFont;
                }
                return Control.defaultFont;
            }
        }

        public static System.Drawing.Color DefaultForeColor
        {
            get
            {
                return SystemColors.ControlText;
            }
        }

        protected virtual System.Windows.Forms.ImeMode DefaultImeMode
        {
            get
            {
                return System.Windows.Forms.ImeMode.Inherit;
            }
        }

        protected virtual System.Windows.Forms.Padding DefaultMargin
        {
            get
            {
                return CommonProperties.DefaultMargin;
            }
        }

        protected virtual System.Drawing.Size DefaultMaximumSize
        {
            get
            {
                return CommonProperties.DefaultMaximumSize;
            }
        }

        protected virtual System.Drawing.Size DefaultMinimumSize
        {
            get
            {
                return CommonProperties.DefaultMinimumSize;
            }
        }

        protected virtual System.Windows.Forms.Padding DefaultPadding
        {
            get
            {
                return System.Windows.Forms.Padding.Empty;
            }
        }

        private System.Windows.Forms.RightToLeft DefaultRightToLeft
        {
            get
            {
                return System.Windows.Forms.RightToLeft.No;
            }
        }

        protected virtual System.Drawing.Size DefaultSize
        {
            get
            {
                return System.Drawing.Size.Empty;
            }
        }

        internal System.Drawing.Color DisabledColor
        {
            get
            {
                System.Drawing.Color color1 = this.BackColor;
                if (color1.A == 0)
                {
                    for (Control control1 = this.ParentInternal; color1.A == 0; control1 = control1.ParentInternal)
                    {
                        if (control1 == null)
                        {
                            color1 = SystemColors.Control;
                            break;
                        }
                        color1 = control1.BackColor;
                    }
                }
                return color1;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlDisplayRectangleDescr"), Browsable(false)]
        public virtual Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRDescription("ControlDisposingDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Disposing
        {
            get
            {
                return this.GetState(0x1000);
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(0), Localizable(true), System.Windows.Forms.SRDescription("ControlDockDescr")]
        public virtual DockStyle Dock
        {
            get
            {
                return DefaultLayout.GetDock(this);
            }
            set
            {
                if (value != this.Dock)
                {
                    this.SuspendLayout();
                    try
                    {
                        DefaultLayout.SetDock(this, value);
                        this.OnDockChanged(EventArgs.Empty);
                    }
                    finally
                    {
                        this.ResumeLayout();
                    }
                }
            }
        }

        [System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlDoubleBufferedDescr")]
        protected virtual bool DoubleBuffered
        {
            get
            {
                return this.GetStyle(ControlStyles.OptimizedDoubleBuffer);
            }
            set
            {
                if (value != this.DoubleBuffered)
                {
                    if (value)
                    {
                        this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, value);
                    }
                    else
                    {
                        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, value);
                    }
                }
            }
        }

        private bool DoubleBufferingEnabled
        {
            get
            {
                return this.GetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint);
            }
        }

        [Localizable(true), System.Windows.Forms.SRCategory("CatBehavior"), DispId(-514), System.Windows.Forms.SRDescription("ControlEnabledDescr")]
        public bool Enabled
        {
            get
            {
                if (!this.GetState(4))
                {
                    return false;
                }
                if (this.ParentInternal == null)
                {
                    return true;
                }
                return this.ParentInternal.Enabled;
            }
            set
            {
                bool flag1 = this.Enabled;
                this.SetState(4, value);
                if (flag1 != value)
                {
                    if (!value)
                    {
                        this.SelectNextIfFocused();
                    }
                    this.OnEnabledChanged(EventArgs.Empty);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlFocusedDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public virtual bool Focused
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    return (System.Windows.Forms.UnsafeNativeMethods.GetFocus() == this.Handle);
                }
                return false;
            }
        }

        [System.Windows.Forms.SRCategory("CatAppearance"), DispId(-512), AmbientValue((string) null), System.Windows.Forms.SRDescription("ControlFontDescr"), Localizable(true)]
        public virtual System.Drawing.Font Font
        {
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType="", MarshalTypeRef=typeof(ActiveXFontMarshaler), MarshalCookie="")]
            get
            {
                System.Drawing.Font font1 = (System.Drawing.Font) this.Properties.GetObject(Control.PropFont);
                if (font1 != null)
                {
                    return font1;
                }
                System.Drawing.Font font2 = this.GetParentFont();
                if (font2 != null)
                {
                    return font2;
                }
                if (this.IsActiveX)
                {
                    font2 = this.ActiveXAmbientFont;
                    if (font2 != null)
                    {
                        return font2;
                    }
                }
                AmbientProperties properties1 = this.AmbientPropertiesService;
                if ((properties1 != null) && (properties1.Font != null))
                {
                    return properties1.Font;
                }
                return Control.DefaultFont;
            }
            [param: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType="", MarshalTypeRef=typeof(ActiveXFontMarshaler), MarshalCookie="")]
            set
            {
                System.Drawing.Font font1 = (System.Drawing.Font) this.Properties.GetObject(Control.PropFont);
                System.Drawing.Font font2 = this.Font;
                bool flag1 = false;
                if (value == null)
                {
                    if (font1 != null)
                    {
                        flag1 = true;
                    }
                }
                else if (font1 == null)
                {
                    flag1 = true;
                }
                else
                {
                    flag1 = !value.Equals(font1);
                }
                if (flag1)
                {
                    this.Properties.SetObject(Control.PropFont, value);
                    if (!font2.Equals(value))
                    {
                        this.DisposeFontHandle();
                        if (this.Properties.ContainsInteger(Control.PropFontHeight))
                        {
                            this.Properties.SetInteger(Control.PropFontHeight, (value == null) ? -1 : value.Height);
                        }
                        using (LayoutTransaction transaction1 = new LayoutTransaction(this.ParentInternal, this, PropertyNames.Font))
                        {
                            this.OnFontChanged(EventArgs.Empty);
                            return;
                        }
                    }
                    if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
                    {
                        this.DisposeFontHandle();
                        this.SetWindowFont();
                    }
                }
            }
        }

        internal IntPtr FontHandle
        {
            get
            {
                System.Drawing.Font font1 = (System.Drawing.Font) this.Properties.GetObject(Control.PropFont);
                if (font1 != null)
                {
                    FontHandleWrapper wrapper1 = (FontHandleWrapper) this.Properties.GetObject(Control.PropFontHandleWrapper);
                    if (wrapper1 == null)
                    {
                        wrapper1 = new FontHandleWrapper(font1);
                        this.Properties.SetObject(Control.PropFontHandleWrapper, wrapper1);
                    }
                    return wrapper1.Handle;
                }
                if (this.parent != null)
                {
                    return this.parent.FontHandle;
                }
                AmbientProperties properties1 = this.AmbientPropertiesService;
                if ((properties1 == null) || (properties1.Font == null))
                {
                    return Control.GetDefaultFontHandleWrapper().Handle;
                }
                FontHandleWrapper wrapper2 = null;
                System.Drawing.Font font2 = (System.Drawing.Font) this.Properties.GetObject(Control.PropCurrentAmbientFont);
                if ((font2 != null) && (font2 == properties1.Font))
                {
                    wrapper2 = (FontHandleWrapper) this.Properties.GetObject(Control.PropFontHandleWrapper);
                }
                else
                {
                    this.Properties.SetObject(Control.PropCurrentAmbientFont, properties1.Font);
                }
                if (wrapper2 == null)
                {
                    wrapper2 = new FontHandleWrapper(properties1.Font);
                    this.Properties.SetObject(Control.PropFontHandleWrapper, wrapper2);
                }
                return wrapper2.Handle;
            }
        }

        protected int FontHeight
        {
            get
            {
                bool flag1;
                int num1 = this.Properties.GetInteger(Control.PropFontHeight, out flag1);
                if (flag1 && (num1 != -1))
                {
                    return num1;
                }
                System.Drawing.Font font1 = (System.Drawing.Font) this.Properties.GetObject(Control.PropFont);
                if (font1 != null)
                {
                    num1 = font1.Height;
                    this.Properties.SetInteger(Control.PropFontHeight, num1);
                    return num1;
                }
                int num2 = -1;
                if ((this.ParentInternal != null) && this.ParentInternal.CanAccessProperties)
                {
                    num2 = this.ParentInternal.FontHeight;
                }
                if (num2 == -1)
                {
                    num2 = this.Font.Height;
                    this.Properties.SetInteger(Control.PropFontHeight, num2);
                }
                return num2;
            }
            set
            {
                this.Properties.SetInteger(Control.PropFontHeight, value);
            }
        }

        [System.Windows.Forms.SRCategory("CatAppearance"), System.Windows.Forms.SRDescription("ControlForeColorDescr"), DispId(-513)]
        public virtual System.Drawing.Color ForeColor
        {
            get
            {
                System.Drawing.Color color1 = this.Properties.GetColor(Control.PropForeColor);
                if (!color1.IsEmpty)
                {
                    return color1;
                }
                Control control1 = this.ParentInternal;
                if ((control1 != null) && control1.CanAccessProperties)
                {
                    return control1.ForeColor;
                }
                System.Drawing.Color color2 = System.Drawing.Color.Empty;
                if (this.IsActiveX)
                {
                    color2 = this.ActiveXAmbientForeColor;
                }
                if (color2.IsEmpty)
                {
                    AmbientProperties properties1 = this.AmbientPropertiesService;
                    if (properties1 != null)
                    {
                        color2 = properties1.ForeColor;
                    }
                }
                if (!color2.IsEmpty)
                {
                    return color2;
                }
                return Control.DefaultForeColor;
            }
            set
            {
                System.Drawing.Color color1 = this.ForeColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropForeColor))
                {
                    this.Properties.SetColor(Control.PropForeColor, value);
                }
                if (!color1.Equals(this.ForeColor))
                {
                    this.OnForeColorChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false), DispId(-515), System.Windows.Forms.SRDescription("ControlHandleDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr Handle
        {
            get
            {
                if ((Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall) && this.InvokeRequired)
                {
                    object[] objArray1 = new object[1] { this.Name } ;
                    throw new InvalidOperationException(System.Windows.Forms.SR.GetString("IllegalCrossThreadCall", objArray1));
                }
                if (!this.IsHandleCreated)
                {
                    this.CreateHandle();
                }
                return this.HandleInternal;
            }
        }

        internal IntPtr HandleInternal
        {
            get
            {
                return this.window.Handle;
            }
        }

        [System.Windows.Forms.SRDescription("ControlHasChildrenDescr"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool HasChildren
        {
            get
            {
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 != null)
                {
                    return (collection1.Count > 0);
                }
                return false;
            }
        }

        internal virtual bool HasMenu
        {
            get
            {
                return false;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), System.Windows.Forms.SRDescription("ControlHeightDescr"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatLayout")]
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.SetBounds(this.x, this.y, this.width, value, BoundsSpecified.Height);
            }
        }

        internal bool HostedInWin32DialogManager
        {
            get
            {
                if (!this.GetState(0x1000000))
                {
                    Control control1 = this.TopMostParent;
                    if (this != control1)
                    {
                        this.SetState(0x2000000, control1.HostedInWin32DialogManager);
                    }
                    else
                    {
                        IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
                        IntPtr ptr2 = ptr1;
                        StringBuilder builder1 = new StringBuilder(0x20);
                        this.SetState(0x2000000, false);
                        while (ptr1 != IntPtr.Zero)
                        {
                            int num1 = System.Windows.Forms.UnsafeNativeMethods.GetClassName(new HandleRef(null, ptr2), null, 0);
                            if (num1 > builder1.Capacity)
                            {
                                builder1.Capacity = num1 + 5;
                            }
                            System.Windows.Forms.UnsafeNativeMethods.GetClassName(new HandleRef(null, ptr2), builder1, builder1.Capacity);
                            if (builder1.ToString() == "#32770")
                            {
                                this.SetState(0x2000000, true);
                                break;
                            }
                            ptr2 = ptr1;
                            ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(null, ptr1));
                        }
                    }
                    this.SetState(0x1000000, true);
                }
                return this.GetState(0x2000000);
            }
        }

        private bool ImeIsSupported
        {
            get
            {
                if (this.DefaultImeMode != System.Windows.Forms.ImeMode.NoControl)
                {
                    return (this.DefaultImeMode != System.Windows.Forms.ImeMode.Disable);
                }
                return false;
            }
        }

        [System.Windows.Forms.SRDescription("ControlIMEModeDescr"), Localizable(true), AmbientValue(-1), System.Windows.Forms.SRCategory("CatBehavior")]
        public System.Windows.Forms.ImeMode ImeMode
        {
            get
            {
                if (!base.DesignMode && this.Focused)
                {
                    this.UpdateCachedImeMode(this.Handle);
                }
                return this.CachedImeMode;
            }
            set
            {
                this.SetImeMode(value);
                this.SetUnrestrictedImeMode(this.CachedImeMode, true);
            }
        }

        internal IntPtr InternalHandle
        {
            get
            {
                if (!this.IsHandleCreated)
                {
                    return IntPtr.Zero;
                }
                return this.Handle;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRDescription("ControlInvokeRequiredDescr")]
        public bool InvokeRequired
        {
            get
            {
                bool flag1;
                using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
                {
                    HandleRef ref1;
                    int num1;
                    if (this.IsHandleCreated)
                    {
                        ref1 = new HandleRef(this, this.Handle);
                    }
                    else
                    {
                        Control control1 = this.FindMarshalingControl();
                        if (!control1.IsHandleCreated)
                        {
                            return false;
                        }
                        ref1 = new HandleRef(control1, control1.Handle);
                    }
                    int num2 = System.Windows.Forms.SafeNativeMethods.GetWindowThreadProcessId(ref1, out num1);
                    int num3 = System.Windows.Forms.SafeNativeMethods.GetCurrentThreadId();
                    flag1 = num2 != num3;
                }
                return flag1;
            }
        }

        [System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlIsAccessibleDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAccessible
        {
            get
            {
                return this.GetState(0x100000);
            }
            set
            {
                this.SetState(0x100000, value);
            }
        }

        internal bool IsActiveX
        {
            get
            {
                return (this.Properties.GetObject(Control.PropActiveXImpl) != null);
            }
        }

        internal virtual bool IsContainerControl
        {
            get
            {
                return false;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlDisposedDescr")]
        public bool IsDisposed
        {
            get
            {
                return this.GetState(0x800);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlHandleCreatedDescr"), Browsable(false)]
        public bool IsHandleCreated
        {
            get
            {
                return (this.window.Handle != IntPtr.Zero);
            }
        }

        internal bool IsIEParent
        {
            get
            {
                if (!this.IsActiveX)
                {
                    return false;
                }
                return this.ActiveXInstance.IsIE;
            }
        }

        internal bool IsLayoutSuspended
        {
            get
            {
                return (this.layoutSuspendCount > 0);
            }
        }

        [System.Windows.Forms.SRDescription("IsMirroredDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatLayout")]
        public bool IsMirrored
        {
            get
            {
                if (!this.IsHandleCreated)
                {
                    System.Windows.Forms.CreateParams params1 = this.CreateParams;
                    this.SetState(0x40000000, (params1.ExStyle & 0x400000) != 0);
                }
                return this.GetState(0x40000000);
            }
        }

        internal virtual bool IsMnemonicsListenerAxSourced
        {
            get
            {
                return false;
            }
        }

        internal bool IsWindowObscured
        {
            get
            {
                if (!this.IsHandleCreated || !this.Visible)
                {
                    return false;
                }
                bool flag1 = false;
                System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT();
                Control control1 = this.ParentInternal;
                if (control1 != null)
                {
                    while (control1.ParentInternal != null)
                    {
                        control1 = control1.ParentInternal;
                    }
                }
                System.Windows.Forms.UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), out rect1);
                System.Drawing.Region region1 = new System.Drawing.Region(Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom));
                try
                {
                    IntPtr ptr2;
                    IntPtr ptr3;
                    if (control1 != null)
                    {
                        ptr3 = control1.Handle;
                    }
                    else
                    {
                        ptr3 = this.Handle;
                    }
                    for (IntPtr ptr1 = ptr3; (ptr2 = System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(null, ptr1), 3)) != IntPtr.Zero; ptr1 = ptr2)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.GetWindowRect(new HandleRef(null, ptr2), out rect1);
                        Rectangle rectangle1 = Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom);
                        if (System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(null, ptr2)))
                        {
                            region1.Exclude(rectangle1);
                        }
                    }
                    Graphics graphics1 = this.CreateGraphics();
                    try
                    {
                        return region1.IsEmpty(graphics1);
                    }
                    finally
                    {
                        graphics1.Dispose();
                    }
                }
                finally
                {
                    region1.Dispose();
                }
                return flag1;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                return DefaultLayout.Instance;
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlLeftDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Left
        {
            get
            {
                return this.x;
            }
            set
            {
                this.SetBounds(value, this.y, this.width, this.height, BoundsSpecified.X);
            }
        }

        [Localizable(true), System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlLocationDescr")]
        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }
            set
            {
                this.SetBounds(value.X, value.Y, this.width, this.height, BoundsSpecified.Location);
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), Localizable(true), System.Windows.Forms.SRDescription("ControlMarginDescr")]
        public System.Windows.Forms.Padding Margin
        {
            get
            {
                return CommonProperties.GetMargin(this);
            }
            set
            {
                value = LayoutUtils.ClampNegativePaddingToZero(value);
                if (value != this.Margin)
                {
                    CommonProperties.SetMargin(this, value);
                    this.OnMarginChanged(EventArgs.Empty);
                }
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlMaximumSizeDescr"), AmbientValue(typeof(System.Drawing.Size), "0, 0")]
        public virtual System.Drawing.Size MaximumSize
        {
            get
            {
                return CommonProperties.GetMaximumSize(this, this.DefaultMaximumSize);
            }
            set
            {
                if (value == System.Drawing.Size.Empty)
                {
                    CommonProperties.ClearMaximumSize(this);
                }
                else if (value != this.MaximumSize)
                {
                    CommonProperties.SetMaximumSize(this, value);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlMinimumSizeDescr"), System.Windows.Forms.SRCategory("CatLayout")]
        public virtual System.Drawing.Size MinimumSize
        {
            get
            {
                return CommonProperties.GetMinimumSize(this, this.DefaultMinimumSize);
            }
            set
            {
                if (value != this.MinimumSize)
                {
                    CommonProperties.SetMinimumSize(this, value);
                }
            }
        }

        public static Keys ModifierKeys
        {
            get
            {
                Keys keys1 = Keys.None;
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x10) < 0)
                {
                    keys1 |= Keys.Shift;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x11) < 0)
                {
                    keys1 |= Keys.Control;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x12) < 0)
                {
                    keys1 |= Keys.Alt;
                }
                return keys1;
            }
        }

        public static System.Windows.Forms.MouseButtons MouseButtons
        {
            get
            {
                System.Windows.Forms.MouseButtons buttons1 = System.Windows.Forms.MouseButtons.None;
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(1) < 0)
                {
                    buttons1 |= System.Windows.Forms.MouseButtons.Left;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(2) < 0)
                {
                    buttons1 |= System.Windows.Forms.MouseButtons.Right;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(4) < 0)
                {
                    buttons1 |= System.Windows.Forms.MouseButtons.Middle;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(5) < 0)
                {
                    buttons1 |= System.Windows.Forms.MouseButtons.XButton1;
                }
                if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(6) < 0)
                {
                    buttons1 |= System.Windows.Forms.MouseButtons.XButton2;
                }
                return buttons1;
            }
        }

        public static Point MousePosition
        {
            get
            {
                System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT();
                System.Windows.Forms.UnsafeNativeMethods.GetCursorPos(point1);
                return new Point(point1.x, point1.y);
            }
        }

        [Browsable(false)]
        public string Name
        {
            get
            {
                string text1 = (string) this.Properties.GetObject(Control.PropName);
                if (string.IsNullOrEmpty(text1))
                {
                    if (this.Site != null)
                    {
                        text1 = this.Site.Name;
                    }
                    if (text1 == null)
                    {
                        text1 = "";
                    }
                }
                return text1;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.Properties.SetObject(Control.PropName, null);
                }
                else
                {
                    this.Properties.SetObject(Control.PropName, value);
                }
            }
        }

        private AccessibleObject NcAccessibilityObject
        {
            get
            {
                AccessibleObject obj1 = (AccessibleObject) this.Properties.GetObject(Control.PropNcAccessibility);
                if (obj1 == null)
                {
                    obj1 = new ControlAccessibleObject(this, 0);
                    this.Properties.SetObject(Control.PropNcAccessibility, obj1);
                }
                return obj1;
            }
        }

        [System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlPaddingDescr"), Localizable(true)]
        public System.Windows.Forms.Padding Padding
        {
            get
            {
                return CommonProperties.GetPadding(this, this.DefaultPadding);
            }
            set
            {
                if (value != this.Padding)
                {
                    CommonProperties.SetPadding(this, value);
                    this.SetState(0x800000, true);
                    using (LayoutTransaction transaction1 = new LayoutTransaction(this.ParentInternal, this, PropertyNames.Padding))
                    {
                        this.OnPaddingChanged(EventArgs.Empty);
                    }
                    if (this.GetState(0x800000))
                    {
                        LayoutTransaction.DoLayout(this, this, PropertyNames.Padding);
                    }
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlParentDescr")]
        public Control Parent
        {
            get
            {
                System.Windows.Forms.IntSecurity.GetParent.Demand();
                return this.ParentInternal;
            }
            set
            {
                this.ParentInternal = value;
            }
        }

        internal ContainerControl ParentContainerControl
        {
            get
            {
                for (Control control1 = this.ParentInternal; control1 != null; control1 = control1.ParentInternal)
                {
                    if (control1 is ContainerControl)
                    {
                        return (control1 as ContainerControl);
                    }
                }
                return null;
            }
        }

        internal virtual Control ParentInternal
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != value)
                {
                    if (value != null)
                    {
                        value.Controls.Add(this);
                    }
                    else
                    {
                        this.parent.Controls.Remove(this);
                    }
                }
            }
        }

        [Browsable(false)]
        public System.Drawing.Size PreferredSize
        {
            get
            {
                return this.GetPreferredSize(System.Drawing.Size.Empty);
            }
        }

        [System.Windows.Forms.SRDescription("ControlProductNameDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProductName
        {
            get
            {
                return this.VersionInfo.ProductName;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), System.Windows.Forms.SRDescription("ControlProductVersionDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProductVersion
        {
            get
            {
                return this.VersionInfo.ProductVersion;
            }
        }

        internal PropertyStore Properties
        {
            get
            {
                return this.propertyStore;
            }
        }

        internal System.Drawing.Color RawBackColor
        {
            get
            {
                return this.Properties.GetColor(Control.PropBackColor);
            }
        }

        [System.Windows.Forms.SRDescription("ControlRecreatingHandleDescr"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatBehavior")]
        public bool RecreatingHandle
        {
            get
            {
                return ((this.state & 0x10) != 0);
            }
        }

        private Control ReflectParent
        {
            get
            {
                return this.reflectParent;
            }
            set
            {
                if (value != null)
                {
                    value.AddReflectChild();
                }
                Control control1 = this.ReflectParent;
                this.reflectParent = value;
                if (control1 != null)
                {
                    control1.RemoveReflectChild();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlRegionDescr")]
        public System.Drawing.Region Region
        {
            get
            {
                return (System.Drawing.Region) this.Properties.GetObject(Control.PropRegion);
            }
            set
            {
                if (this.GetState(0x80000))
                {
                    System.Windows.Forms.IntSecurity.ChangeWindowRegionForTopLevel.Demand();
                }
                System.Drawing.Region region1 = this.Region;
                if (region1 != value)
                {
                    this.Properties.SetObject(Control.PropRegion, value);
                    if (region1 != null)
                    {
                        region1.Dispose();
                    }
                    if (this.IsHandleCreated)
                    {
                        IntPtr ptr1 = IntPtr.Zero;
                        try
                        {
                            if (value != null)
                            {
                                ptr1 = this.GetHRgn(value);
                            }
                            if (this.IsActiveX)
                            {
                                ptr1 = this.ActiveXMergeRegion(ptr1);
                            }
                            if (System.Windows.Forms.UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, ptr1), System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
                            {
                                ptr1 = IntPtr.Zero;
                            }
                        }
                        finally
                        {
                            if (ptr1 != IntPtr.Zero)
                            {
                                System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(null, ptr1));
                            }
                        }
                    }
                    this.OnRegionChanged(EventArgs.Empty);
                }
            }
        }

        [Obsolete("This property has been deprecated. Please use RightToLeft instead. http://go.microsoft.com/fwlink/?linkid=14202")]
        protected internal bool RenderRightToLeft
        {
            get
            {
                return true;
            }
        }

        internal virtual bool RenderTransparencyWithVisualStyles
        {
            get
            {
                return false;
            }
        }

        internal bool RenderTransparent
        {
            get
            {
                if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
                {
                    return (this.BackColor.A < 0xff);
                }
                return false;
            }
        }

        internal BoundsSpecified RequiredScaling
        {
            get
            {
                if ((this.requiredScaling & 0x10) != 0)
                {
                    return (((BoundsSpecified) this.requiredScaling) & BoundsSpecified.All);
                }
                return BoundsSpecified.None;
            }
            set
            {
                byte num1 = (byte) (this.requiredScaling & 0x10);
                this.requiredScaling = (byte) ((value & BoundsSpecified.All) | ((BoundsSpecified) num1));
            }
        }

        internal bool RequiredScalingEnabled
        {
            get
            {
                return ((this.requiredScaling & 0x10) != 0);
            }
            set
            {
                byte num1 = (byte) (this.requiredScaling & 15);
                this.requiredScaling = num1;
                if (value)
                {
                    this.requiredScaling = (byte) (this.requiredScaling | 0x10);
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlResizeRedrawDescr")]
        protected bool ResizeRedraw
        {
            get
            {
                return this.GetStyle(ControlStyles.ResizeRedraw);
            }
            set
            {
                this.SetStyle(ControlStyles.ResizeRedraw, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatLayout"), System.Windows.Forms.SRDescription("ControlRightDescr"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public int Right
        {
            get
            {
                return (this.x + this.width);
            }
        }

        [Localizable(true), AmbientValue(2), System.Windows.Forms.SRDescription("ControlRightToLeftDescr"), System.Windows.Forms.SRCategory("CatAppearance")]
        public virtual System.Windows.Forms.RightToLeft RightToLeft
        {
            get
            {
                bool flag1;
                int num1 = this.Properties.GetInteger(Control.PropRightToLeft, out flag1);
                if (!flag1)
                {
                    num1 = 2;
                }
                if (num1 == 2)
                {
                    Control control1 = this.ParentInternal;
                    if (control1 != null)
                    {
                        num1 = (int) control1.RightToLeft;
                    }
                    else
                    {
                        num1 = (int) this.DefaultRightToLeft;
                    }
                }
                return (System.Windows.Forms.RightToLeft) num1;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, System.Windows.Forms.RightToLeft.No, System.Windows.Forms.RightToLeft.Inherit))
                {
                    throw new InvalidEnumArgumentException("RightToLeft", (int) value, typeof(System.Windows.Forms.RightToLeft));
                }
                System.Windows.Forms.RightToLeft left1 = this.RightToLeft;
                if (this.Properties.ContainsInteger(Control.PropRightToLeft) || (value != System.Windows.Forms.RightToLeft.Inherit))
                {
                    this.Properties.SetInteger(Control.PropRightToLeft, (int) value);
                }
                if (left1 != this.RightToLeft)
                {
                    using (LayoutTransaction transaction1 = new LayoutTransaction(this, this, PropertyNames.RightToLeft))
                    {
                        this.OnRightToLeftChanged(EventArgs.Empty);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual bool ScaleChildren
        {
            get
            {
                return true;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldAutoValidate
        {
            get
            {
                return (Control.GetAutoValidateForControl(this) != AutoValidate.Disable);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected internal virtual bool ShowFocusCues
        {
            get
            {
                if (!this.IsHandleCreated)
                {
                    return true;
                }
                if ((this.uiCuesState & 15) == 0)
                {
                    if (SystemInformation.MenuAccessKeysUnderlined)
                    {
                        this.uiCuesState |= 2;
                    }
                    else
                    {
                        this.uiCuesState |= 1;
                        int num1 = 0x30000;
                        System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 0x127, (IntPtr) (num1 | 1), IntPtr.Zero);
                    }
                }
                return ((this.uiCuesState & 15) == 2);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual bool ShowKeyboardCues
        {
            get
            {
                if (!this.IsHandleCreated || base.DesignMode)
                {
                    return true;
                }
                if ((this.uiCuesState & 240) == 0)
                {
                    if (SystemInformation.MenuAccessKeysUnderlined)
                    {
                        this.uiCuesState |= 0x20;
                    }
                    else
                    {
                        int num1 = 0x30000;
                        this.uiCuesState |= 0x10;
                        System.Windows.Forms.UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 0x127, (IntPtr) (num1 | 1), IntPtr.Zero);
                    }
                }
                return ((this.uiCuesState & 240) == 0x20);
            }
        }

        internal virtual int ShowParams
        {
            get
            {
                return 5;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                AmbientProperties properties1 = this.AmbientPropertiesService;
                AmbientProperties properties2 = null;
                if (value != null)
                {
                    properties2 = (AmbientProperties) value.GetService(typeof(AmbientProperties));
                }
                if (properties1 != properties2)
                {
                    bool flag1 = !this.Properties.ContainsObject(Control.PropFont);
                    bool flag2 = !this.Properties.ContainsObject(Control.PropBackColor);
                    bool flag3 = !this.Properties.ContainsObject(Control.PropForeColor);
                    bool flag4 = !this.Properties.ContainsObject(Control.PropCursor);
                    System.Drawing.Font font1 = null;
                    System.Drawing.Color color1 = System.Drawing.Color.Empty;
                    System.Drawing.Color color2 = System.Drawing.Color.Empty;
                    System.Windows.Forms.Cursor cursor1 = null;
                    if (flag1)
                    {
                        font1 = this.Font;
                    }
                    if (flag2)
                    {
                        color1 = this.BackColor;
                    }
                    if (flag3)
                    {
                        color2 = this.ForeColor;
                    }
                    if (flag4)
                    {
                        cursor1 = this.Cursor;
                    }
                    this.Properties.SetObject(Control.PropAmbientPropertiesService, properties2);
                    base.Site = value;
                    if (flag1 && !font1.Equals(this.Font))
                    {
                        this.OnFontChanged(EventArgs.Empty);
                    }
                    if (flag3 && !color2.Equals(this.ForeColor))
                    {
                        this.OnForeColorChanged(EventArgs.Empty);
                    }
                    if (flag2 && !color1.Equals(this.BackColor))
                    {
                        this.OnBackColorChanged(EventArgs.Empty);
                    }
                    if (flag4 && cursor1.Equals(this.Cursor))
                    {
                        this.OnCursorChanged(EventArgs.Empty);
                    }
                }
                else
                {
                    base.Site = value;
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlSizeDescr"), Localizable(true), System.Windows.Forms.SRCategory("CatLayout")]
        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.width, this.height);
            }
            set
            {
                this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
            }
        }

        internal virtual bool SupportsUseCompatibleTextRendering
        {
            get
            {
                return false;
            }
        }

        ArrangedElementCollection IArrangedElement.Children
        {
            get
            {
                System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                if (collection1 == null)
                {
                    return ArrangedElementCollection.Empty;
                }
                return collection1;
            }
        }

        IArrangedElement IArrangedElement.Container
        {
            get
            {
                return this.ParentInternal;
            }
        }

        bool IArrangedElement.ParticipatesInLayout
        {
            get
            {
                if (!this.GetState(2))
                {
                    return base.DesignMode;
                }
                return true;
            }
        }

        PropertyStore IArrangedElement.Properties
        {
            get
            {
                return this.Properties;
            }
        }

        [System.Windows.Forms.SRDescription("ControlTabIndexDescr"), MergableProperty(false), Localizable(true), System.Windows.Forms.SRCategory("CatBehavior")]
        public int TabIndex
        {
            get
            {
                if (this.tabIndex != -1)
                {
                    return this.tabIndex;
                }
                return 0;
            }
            set
            {
                if (value < 0)
                {
                    object[] objArray1 = new object[3];
                    objArray1[0] = "TabIndex";
                    objArray1[1] = value.ToString(CultureInfo.CurrentCulture);
                    int num1 = 0;
                    objArray1[2] = num1.ToString(CultureInfo.CurrentCulture);
                    throw new ArgumentOutOfRangeException("TabIndex", System.Windows.Forms.SR.GetString("InvalidLowBoundArgumentEx", objArray1));
                }
                if (this.tabIndex != value)
                {
                    this.tabIndex = value;
                    this.OnTabIndexChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue(true), System.Windows.Forms.SRDescription("ControlTabStopDescr"), System.Windows.Forms.SRCategory("CatBehavior"), DispId(-516)]
        public bool TabStop
        {
            get
            {
                return ((this.state & 8) != 0);
            }
            set
            {
                if (this.TabStop != value)
                {
                    this.SetState(8, value);
                    if (this.IsHandleCreated)
                    {
                        this.SetWindowStyle(0x10000, value);
                    }
                    this.OnTabStopChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue((string) null), TypeConverter(typeof(StringConverter)), System.Windows.Forms.SRCategory("CatData"), Localizable(false), System.Windows.Forms.SRDescription("ControlTagDescr"), Bindable(true)]
        public object Tag
        {
            get
            {
                return this.Properties.GetObject(Control.PropUserData);
            }
            set
            {
                this.Properties.SetObject(Control.PropUserData, value);
            }
        }

        [System.Windows.Forms.SRCategory("CatAppearance"), System.Windows.Forms.SRDescription("ControlTextDescr"), Localizable(true), Bindable(true), DispId(-517)]
        public virtual string Text
        {
            get
            {
                if (!this.CacheTextInternal)
                {
                    return this.WindowText;
                }
                if (this.text != null)
                {
                    return this.text;
                }
                return "";
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value != this.Text)
                {
                    if (this.CacheTextInternal)
                    {
                        this.text = value;
                    }
                    this.WindowText = value;
                    this.OnTextChanged(EventArgs.Empty);
                    if (this.IsMnemonicsListenerAxSourced)
                    {
                        for (Control control1 = this; control1 != null; control1 = control1.ParentInternal)
                        {
                            ActiveXImpl impl1 = (ActiveXImpl) control1.Properties.GetObject(Control.PropActiveXImpl);
                            if (impl1 != null)
                            {
                                impl1.UpdateAccelTable();
                                return;
                            }
                        }
                    }
                }
            }
        }

        [System.Windows.Forms.SRDescription("ControlTopDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatLayout"), Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public int Top
        {
            get
            {
                return this.y;
            }
            set
            {
                this.SetBounds(this.x, value, this.width, this.height, BoundsSpecified.Y);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRDescription("ControlTopLevelControlDescr"), System.Windows.Forms.SRCategory("CatBehavior")]
        public Control TopLevelControl
        {
            get
            {
                System.Windows.Forms.IntSecurity.GetParent.Demand();
                return this.TopLevelControlInternal;
            }
        }

        internal Control TopLevelControlInternal
        {
            get
            {
                Control control1 = this;
                while ((control1 != null) && !control1.GetTopLevel())
                {
                    control1 = control1.ParentInternal;
                }
                return control1;
            }
        }

        internal Control TopMostParent
        {
            get
            {
                Control control1 = this;
                while (control1.ParentInternal != null)
                {
                    control1 = control1.ParentInternal;
                }
                return control1;
            }
        }

        private System.Windows.Forms.ImeMode UnrestrictedImeMode
        {
            get
            {
                bool flag1;
                System.Windows.Forms.ImeMode mode1 = (System.Windows.Forms.ImeMode) this.Properties.GetInteger(Control.PropUnrestrictedImeMode, out flag1);
                if (!flag1)
                {
                    mode1 = this.CachedImeMode;
                    this.SetUnrestrictedImeMode(mode1, true);
                }
                return mode1;
            }
            set
            {
                this.SetUnrestrictedImeMode(value, false);
            }
        }

        private bool UpdatingCachedIme
        {
            get
            {
                bool flag1;
                int num1 = this.Properties.GetInteger(Control.PropUpdatingCachedIme, out flag1);
                if (!flag1)
                {
                    return false;
                }
                return (num1 == 1);
            }
            set
            {
                this.Properties.SetInteger(Control.PropUpdatingCachedIme, value ? 1 : 0);
            }
        }

        internal bool UseCompatibleTextRenderingInt
        {
            get
            {
                if (this.Properties.ContainsInteger(Control.PropUseCompatibleTextRendering))
                {
                    bool flag1;
                    int num1 = this.Properties.GetInteger(Control.PropUseCompatibleTextRendering, out flag1);
                    if (flag1)
                    {
                        return (num1 == 1);
                    }
                }
                return Control.UseCompatibleTextRenderingDefault;
            }
            set
            {
                if (this.SupportsUseCompatibleTextRendering && (this.UseCompatibleTextRenderingInt != value))
                {
                    this.Properties.SetInteger(Control.PropUseCompatibleTextRendering, value ? 1 : 0);
                    LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.UseCompatibleTextRendering);
                    this.Invalidate();
                }
            }
        }

        [System.Windows.Forms.SRCategory("CatAppearance"), Browsable(true), DefaultValue(false), System.Windows.Forms.SRDescription("ControlUseWaitCursorDescr"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool UseWaitCursor
        {
            get
            {
                return this.GetState(0x400);
            }
            set
            {
                if (this.GetState(0x400) != value)
                {
                    this.SetState(0x400, value);
                    System.Windows.Forms.Control.ControlCollection collection1 = (System.Windows.Forms.Control.ControlCollection) this.Properties.GetObject(Control.PropControlsCollection);
                    if (collection1 != null)
                    {
                        for (int num1 = 0; num1 < collection1.Count; num1++)
                        {
                            collection1[num1].UseWaitCursor = value;
                        }
                    }
                }
            }
        }

        internal bool ValidationCancelled
        {
            get
            {
                if (this.GetState(0x10000000))
                {
                    return true;
                }
                Control control1 = this.ParentInternal;
                if (control1 != null)
                {
                    return control1.ValidationCancelled;
                }
                return false;
            }
            set
            {
                this.SetState(0x10000000, value);
            }
        }

        private ControlVersionInfo VersionInfo
        {
            get
            {
                ControlVersionInfo info1 = (ControlVersionInfo) this.Properties.GetObject(Control.PropControlVersionInfo);
                if (info1 == null)
                {
                    info1 = new ControlVersionInfo(this);
                    this.Properties.SetObject(Control.PropControlVersionInfo, info1);
                }
                return info1;
            }
        }

        [System.Windows.Forms.SRCategory("CatBehavior"), System.Windows.Forms.SRDescription("ControlVisibleDescr"), Localizable(true)]
        public bool Visible
        {
            get
            {
                return this.GetVisibleCore();
            }
            set
            {
                this.SetVisibleCore(value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Always), System.Windows.Forms.SRDescription("ControlWidthDescr"), System.Windows.Forms.SRCategory("CatLayout"), Browsable(false)]
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.SetBounds(this.x, this.y, value, this.height, BoundsSpecified.Width);
            }
        }

        private int WindowExStyle
        {
            get
            {
                return (int) ((long) System.Windows.Forms.UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -20));
            }
            set
            {
                System.Windows.Forms.UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -20, new HandleRef(null, (IntPtr) value));
            }
        }

        internal int WindowStyle
        {
            get
            {
                return (int) ((long) System.Windows.Forms.UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16));
            }
            set
            {
                System.Windows.Forms.UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr) value));
            }
        }

        [System.Windows.Forms.SRDescription("ControlWindowTargetDescr"), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Windows.Forms.SRCategory("CatBehavior"), Browsable(false)]
        public IWindowTarget WindowTarget
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                return this.window.WindowTarget;
            }
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            set
            {
                this.window.WindowTarget = value;
            }
        }

        internal virtual string WindowText
        {
            get
            {
                string text1;
                if (!this.IsHandleCreated)
                {
                    if (this.text == null)
                    {
                        return "";
                    }
                    return this.text;
                }
                using (MultithreadSafeCallScope scope1 = new MultithreadSafeCallScope())
                {
                    int num1 = System.Windows.Forms.SafeNativeMethods.GetWindowTextLength(new HandleRef(this.window, this.Handle));
                    if (SystemInformation.DbcsEnabled)
                    {
                        num1 = (num1 * 2) + 1;
                    }
                    StringBuilder builder1 = new StringBuilder(num1 + 1);
                    System.Windows.Forms.UnsafeNativeMethods.GetWindowText(new HandleRef(this.window, this.Handle), builder1, builder1.Capacity);
                    text1 = builder1.ToString();
                }
                return text1;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (!this.WindowText.Equals(value))
                {
                    if (this.IsHandleCreated)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.SetWindowText(new HandleRef(this.window, this.Handle), value);
                    }
                    else if (value.Length == 0)
                    {
                        this.text = null;
                    }
                    else
                    {
                        this.text = value;
                    }
                }
            }
        }


        // Fields
        internal static readonly BooleanSwitch BufferPinkRect;
        private LayoutEventArgs cachedLayoutEventArgs;
        private static bool checkForIllegalCrossThreadCalls;
        private int clientHeight;
        private int clientWidth;
        internal static readonly TraceSwitch ControlKeyboardRouting;
        private ControlStyles controlStyle;
        private System.Windows.Forms.CreateParams createParams;
        [ThreadStatic]
        internal static HelpInfo currentHelpInfo;
        private static System.Drawing.Font defaultFont;
        private static FontHandleWrapper defaultFontHandleWrapper;
        private static readonly object EventAutoSizeChanged;
        private static readonly object EventBackColor;
        private static readonly object EventBackgroundImage;
        private static readonly object EventBackgroundImageLayout;
        private static readonly object EventBindingContext;
        private static readonly object EventCausesValidation;
        private static readonly object EventChangeUICues;
        private static readonly object EventClick;
        private static readonly object EventClientSize;
        private static readonly object EventContextMenu;
        private static readonly object EventContextMenuStrip;
        private static readonly object EventControlAdded;
        private static readonly object EventControlRemoved;
        private static readonly object EventCursor;
        private static readonly object EventDock;
        private static readonly object EventDoubleClick;
        private static readonly object EventDragDrop;
        private static readonly object EventDragEnter;
        private static readonly object EventDragLeave;
        private static readonly object EventDragOver;
        private static readonly object EventEnabled;
        private static readonly object EventEnabledChanged;
        private static readonly object EventEnter;
        private static readonly object EventFont;
        private static readonly object EventForeColor;
        private static readonly object EventGiveFeedback;
        private static readonly object EventGotFocus;
        private static readonly object EventHandleCreated;
        private static readonly object EventHandleDestroyed;
        private static readonly object EventHelpRequested;
        private static readonly object EventImeModeChanged;
        private static readonly object EventInvalidated;
        private static readonly object EventKeyDown;
        private static readonly object EventKeyPress;
        private static readonly object EventKeyUp;
        private static readonly object EventLayout;
        private static readonly object EventLeave;
        private static readonly object EventLocation;
        private static readonly object EventLostFocus;
        private static readonly object EventMarginChanged;
        private static readonly object EventMouseCaptureChanged;
        private static readonly object EventMouseClick;
        private static readonly object EventMouseDoubleClick;
        private static readonly object EventMouseDown;
        private static readonly object EventMouseEnter;
        private static readonly object EventMouseHover;
        private static readonly object EventMouseLeave;
        private static readonly object EventMouseMove;
        private static readonly object EventMouseUp;
        private static readonly object EventMouseWheel;
        private static readonly object EventMove;
        internal static readonly object EventPaddingChanged;
        private static readonly object EventPaint;
        private static readonly object EventParent;
        private static readonly object EventPreviewKeyDown;
        private static readonly object EventQueryAccessibilityHelp;
        private static readonly object EventQueryContinueDrag;
        private static readonly object EventRegionChanged;
        private static readonly object EventResize;
        private static readonly object EventRightToLeft;
        private static readonly object EventSize;
        private static readonly object EventStyleChanged;
        private static readonly object EventSystemColorsChanged;
        private static readonly object EventTabIndex;
        private static readonly object EventTabStop;
        private static readonly object EventText;
        private static readonly object EventValidated;
        private static readonly object EventValidating;
        private static readonly object EventVisible;
        private static readonly object EventVisibleChanged;
        internal static readonly TraceSwitch FocusTracing;
        private int height;
        [ThreadStatic]
        private static bool inCrossThreadSafeCall;
        private static ContextCallback invokeMarshaledCallbackHelperDelegate;
        private byte layoutSuspendCount;
        private static bool mouseWheelInit;
        private static int mouseWheelMessage;
        private static bool mouseWheelRoutingNeeded;
        private static IntPtr originalImeContext;
        private const short PaintLayerBackground = 1;
        private const short PaintLayerForeground = 2;
        internal static readonly TraceSwitch PaletteTracing;
        private Control parent;
        private static readonly int PropAccessibility;
        private static readonly int PropAccessibleDefaultActionDescription;
        private static readonly int PropAccessibleDescription;
        private static readonly int PropAccessibleHelpProvider;
        private static readonly int PropAccessibleName;
        private static readonly int PropAccessibleRole;
        private static readonly int PropActiveXImpl;
        private static readonly int PropAmbientPropertiesService;
        private static readonly int PropAutoScrollOffset;
        private static readonly int PropBackBrush;
        private static readonly int PropBackColor;
        private static readonly int PropBackgroundImage;
        private static readonly int PropBackgroundImageLayout;
        private static readonly int PropBindingManager;
        private static readonly int PropBindings;
        private static readonly int PropCacheTextCount;
        private static readonly int PropCacheTextField;
        private static readonly int PropCharsToIgnore;
        private static readonly int PropContextMenu;
        private static readonly int PropContextMenuStrip;
        private static readonly int PropControlsCollection;
        private static readonly int PropControlVersionInfo;
        private static readonly int PropCurrentAmbientFont;
        private static readonly int PropCursor;
        private PropertyStore propertyStore;
        private static readonly int PropFont;
        private static readonly int PropFontHandleWrapper;
        private static readonly int PropFontHeight;
        private static readonly int PropForeColor;
        private static readonly int PropImeMode;
        private static readonly int PropName;
        private static readonly int PropNcAccessibility;
        private static readonly int PropPaintingException;
        private static readonly int PropRegion;
        private static readonly int PropRightToLeft;
        private static readonly int PropThreadCallbackList;
        private static readonly int PropUnrestrictedImeMode;
        private static readonly int PropUpdatingCachedIme;
        private static readonly int PropUseCompatibleTextRendering;
        private static readonly int PropUserData;
        private Control reflectParent;
        private byte requiredScaling;
        private const byte RequiredScalingEnabledMask = 0x10;
        private const byte RequiredScalingMask = 15;
        private int state;
        internal const int STATE_ALLOWDROP = 0x40;
        internal const int STATE_CAUSESVALIDATION = 0x20000;
        internal const int STATE_CHECKEDHOST = 0x1000000;
        internal const int STATE_CREATED = 1;
        internal const int STATE_CREATINGHANDLE = 0x40000;
        internal const int STATE_DISPOSED = 0x800;
        internal const int STATE_DISPOSING = 0x1000;
        internal const int STATE_DOUBLECLICKFIRED = 0x4000000;
        internal const int STATE_DROPTARGET = 0x80;
        internal const int STATE_ENABLED = 4;
        internal const int STATE_EXCEPTIONWHILEPAINTING = 0x400000;
        internal const int STATE_HOSTEDINDIALOG = 0x2000000;
        internal const int STATE_ISACCESSIBLE = 0x100000;
        internal const int STATE_LAYOUTDEFERRED = 0x200;
        internal const int STATE_LAYOUTISDIRTY = 0x800000;
        internal const int STATE_MIRRORED = 0x40000000;
        internal const int STATE_MODAL = 0x20;
        internal const int STATE_MOUSEENTERPENDING = 0x2000;
        internal const int STATE_MOUSEPRESSED = 0x8000000;
        internal const int STATE_NOZORDER = 0x100;
        internal const int STATE_OWNCTLBRUSH = 0x200000;
        internal const int STATE_PARENTRECREATING = 0x20000000;
        internal const int STATE_RECREATE = 0x10;
        internal const int STATE_SIZELOCKEDBYOS = 0x10000;
        internal const int STATE_TABSTOP = 8;
        internal const int STATE_THREADMARSHALLPENDING = 0x8000;
        internal const int STATE_TOPLEVEL = 0x80000;
        internal const int STATE_TRACKINGMOUSEEVENT = 0x4000;
        internal const int STATE_USEWAITCURSOR = 0x400;
        internal const int STATE_VALIDATIONCANCELLED = 0x10000000;
        internal const int STATE_VISIBLE = 2;
        private int state2;
        private const int STATE2_BECOMINGACTIVECONTROL = 0x20;
        private const int STATE2_CLEARLAYOUTARGS = 0x40;
        private const int STATE2_HAVEINVOKED = 1;
        private const int STATE2_INPUTCHAR = 0x100;
        private const int STATE2_INPUTKEY = 0x80;
        internal const int STATE2_INTERESTEDINUSERPREFERENCECHANGED = 8;
        private const int STATE2_LISTENINGTOUSERPREFERENCECHANGED = 4;
        internal const int STATE2_MAINTAINSOWNCAPTUREMODE = 0x10;
        private const int STATE2_SETSCROLLPOS = 2;
        private const int STATE2_UICUES = 0x200;
        private int tabIndex;
        private string text;
        private static int threadCallbackMessage;
        private System.Windows.Forms.NativeMethods.TRACKMOUSEEVENT trackMouseEvent;
        private int uiCuesState;
        private const int UISTATE_FOCUS_CUES_HIDDEN = 1;
        private const int UISTATE_FOCUS_CUES_MASK = 15;
        private const int UISTATE_FOCUS_CUES_SHOW = 2;
        private const int UISTATE_KEYBOARD_CUES_HIDDEN = 0x10;
        private const int UISTATE_KEYBOARD_CUES_MASK = 240;
        private const int UISTATE_KEYBOARD_CUES_SHOW = 0x20;
        private short updateCount;
        internal static bool UseCompatibleTextRenderingDefault;
        private int width;
        private ControlNativeWindow window;
        private static int WM_GETCONTROLNAME;
        private static int WM_GETCONTROLTYPE;
        private int x;
        private int y;

        // Nested Types
        private class ActiveXFontMarshaler : ICustomMarshaler
        {
            // Methods
            public ActiveXFontMarshaler()
            {
            }

            public void CleanUpManagedData(object obj)
            {
            }

            public void CleanUpNativeData(IntPtr pObj)
            {
                Marshal.Release(pObj);
            }

            internal static ICustomMarshaler GetInstance(string cookie)
            {
                if (Control.ActiveXFontMarshaler.instance == null)
                {
                    Control.ActiveXFontMarshaler.instance = new Control.ActiveXFontMarshaler();
                }
                return Control.ActiveXFontMarshaler.instance;
            }

            public int GetNativeDataSize()
            {
                return -1;
            }

            public IntPtr MarshalManagedToNative(object obj)
            {
                IntPtr ptr2;
                Font font1 = (Font) obj;
                System.Windows.Forms.NativeMethods.tagFONTDESC gfontdesc1 = new System.Windows.Forms.NativeMethods.tagFONTDESC();
                System.Windows.Forms.NativeMethods.LOGFONT logfont1 = new System.Windows.Forms.NativeMethods.LOGFONT();
                System.Windows.Forms.IntSecurity.ObjectFromWin32Handle.Assert();
                try
                {
                    font1.ToLogFont(logfont1);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                gfontdesc1.lpstrName = font1.Name;
                gfontdesc1.cySize = (long) (font1.SizeInPoints * 10000f);
                gfontdesc1.sWeight = (short) logfont1.lfWeight;
                gfontdesc1.sCharset = logfont1.lfCharSet;
                gfontdesc1.fItalic = font1.Italic;
                gfontdesc1.fUnderline = font1.Underline;
                gfontdesc1.fStrikethrough = font1.Strikeout;
                Guid guid1 = typeof(System.Windows.Forms.UnsafeNativeMethods.IFont).GUID;
                System.Windows.Forms.UnsafeNativeMethods.IFont font2 = System.Windows.Forms.UnsafeNativeMethods.OleCreateFontIndirect(gfontdesc1, ref guid1);
                IntPtr ptr1 = Marshal.GetIUnknownForObject(font2);
                int num1 = Marshal.QueryInterface(ptr1, ref guid1, out ptr2);
                Marshal.Release(ptr1);
                if (System.Windows.Forms.NativeMethods.Failed(num1))
                {
                    Marshal.ThrowExceptionForHR(num1);
                }
                return ptr2;
            }

            public object MarshalNativeToManaged(IntPtr pObj)
            {
                System.Windows.Forms.UnsafeNativeMethods.IFont font1 = (System.Windows.Forms.UnsafeNativeMethods.IFont) Marshal.GetObjectForIUnknown(pObj);
                IntPtr ptr1 = IntPtr.Zero;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    ptr1 = font1.GetHFont();
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                Font font2 = null;
                System.Windows.Forms.IntSecurity.ObjectFromWin32Handle.Assert();
                try
                {
                    font2 = Font.FromHfont(ptr1);
                }
                catch (Exception exception1)
                {
                    if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                    {
                        throw;
                    }
                    font2 = Control.DefaultFont;
                }
                catch
                {
                    font2 = Control.DefaultFont;
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                return font2;
            }


            // Fields
            private static Control.ActiveXFontMarshaler instance;
        }

        private class ActiveXImpl : MarshalByRefObject, IWindowTarget
        {
            // Methods
            static ActiveXImpl()
            {
                Control.ActiveXImpl.hiMetricPerInch = 0x9ec;
                Control.ActiveXImpl.viewAdviseOnlyOnce = BitVector32.CreateMask();
                Control.ActiveXImpl.viewAdvisePrimeFirst = BitVector32.CreateMask(Control.ActiveXImpl.viewAdviseOnlyOnce);
                Control.ActiveXImpl.eventsFrozen = BitVector32.CreateMask(Control.ActiveXImpl.viewAdvisePrimeFirst);
                Control.ActiveXImpl.changingExtents = BitVector32.CreateMask(Control.ActiveXImpl.eventsFrozen);
                Control.ActiveXImpl.saving = BitVector32.CreateMask(Control.ActiveXImpl.changingExtents);
                Control.ActiveXImpl.isDirty = BitVector32.CreateMask(Control.ActiveXImpl.saving);
                Control.ActiveXImpl.inPlaceActive = BitVector32.CreateMask(Control.ActiveXImpl.isDirty);
                Control.ActiveXImpl.inPlaceVisible = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceActive);
                Control.ActiveXImpl.uiActive = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceVisible);
                Control.ActiveXImpl.uiDead = BitVector32.CreateMask(Control.ActiveXImpl.uiActive);
                Control.ActiveXImpl.adjustingRect = BitVector32.CreateMask(Control.ActiveXImpl.uiDead);
                Control.ActiveXImpl.logPixels = Point.Empty;
                Control.ActiveXImpl.globalActiveXCount = 0;
            }

            internal ActiveXImpl(Control control)
            {
                this.accelCount = -1;
                this.control = control;
                this.controlWindowTarget = control.WindowTarget;
                control.WindowTarget = this;
                this.adviseList = new ArrayList();
                this.activeXState = new BitVector32();
                Control.AmbientProperty[] propertyArray1 = new Control.AmbientProperty[3] { new Control.AmbientProperty("Font", -703), new Control.AmbientProperty("BackColor", -701), new Control.AmbientProperty("ForeColor", -704) } ;
                this.ambientProperties = propertyArray1;
            }

            internal int Advise(IAdviseSink pAdvSink)
            {
                this.adviseList.Add(pAdvSink);
                return this.adviseList.Count;
            }

            private void CallParentPropertyChanged(Control control, string propName)
            {
                string text1;
                if ((text1 = propName) != null)
                {
                    int num1;
                    if (<PrivateImplementationDetails>{BB83A869-DCF6-4F9E-8D21-755D7805076D}.$$method0x60016c4-1 == null)
                    {
                        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(8);
                        dictionary1.Add("BackColor", 0);
                        dictionary1.Add("BackgroundImage", 1);
                        dictionary1.Add("BindingContext", 2);
                        dictionary1.Add("Enabled", 3);
                        dictionary1.Add("Font", 4);
                        dictionary1.Add("ForeColor", 5);
                        dictionary1.Add("RightToLeft", 6);
                        dictionary1.Add("Visible", 7);
                        <PrivateImplementationDetails>{BB83A869-DCF6-4F9E-8D21-755D7805076D}.$$method0x60016c4-1 = dictionary1;
                    }
                    if (!<PrivateImplementationDetails>{BB83A869-DCF6-4F9E-8D21-755D7805076D}.$$method0x60016c4-1.TryGetValue((string) text1, ref (int) ref num1))
                    {
                        return;
                    }
                    switch (num1)
                    {
                        case 0:
                        {
                            control.OnParentBackColorChanged(EventArgs.Empty);
                            return;
                        }
                        case 1:
                        {
                            control.OnParentBackgroundImageChanged(EventArgs.Empty);
                            return;
                        }
                        case 2:
                        {
                            control.OnParentBindingContextChanged(EventArgs.Empty);
                            return;
                        }
                        case 3:
                        {
                            control.OnParentEnabledChanged(EventArgs.Empty);
                            return;
                        }
                        case 4:
                        {
                            control.OnParentFontChanged(EventArgs.Empty);
                            return;
                        }
                        case 5:
                        {
                            control.OnParentForeColorChanged(EventArgs.Empty);
                            return;
                        }
                        case 6:
                        {
                            control.OnParentRightToLeftChanged(EventArgs.Empty);
                            return;
                        }
                        case 7:
                        {
                            control.OnParentVisibleChanged(EventArgs.Empty);
                            return;
                        }
                    }
                }
            }

            internal void Close(int dwSaveOption)
            {
                if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
                {
                    this.InPlaceDeactivate();
                }
                if (((dwSaveOption == 0) || (dwSaveOption == 2)) && this.activeXState[Control.ActiveXImpl.isDirty])
                {
                    if (this.clientSite != null)
                    {
                        this.clientSite.SaveObject();
                    }
                    this.SendOnSave();
                }
            }

            internal void DoVerb(int iVerb, IntPtr lpmsg, System.Windows.Forms.UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, System.Windows.Forms.NativeMethods.COMRECT lprcPosRect)
            {
                switch (iVerb)
                {
                    case -5:
                    case -4:
                    case -1:
                    case 0:
                    {
                        this.InPlaceActivate(iVerb);
                        if (lpmsg != IntPtr.Zero)
                        {
                            System.Windows.Forms.NativeMethods.MSG msg1 = (System.Windows.Forms.NativeMethods.MSG) System.Windows.Forms.UnsafeNativeMethods.PtrToStructure(lpmsg, typeof(System.Windows.Forms.NativeMethods.MSG));
                            Control control1 = this.control;
                            if (((msg1.hwnd != this.control.Handle) && (msg1.message >= 0x200)) && (msg1.message <= 0x20a))
                            {
                                IntPtr ptr1 = (msg1.hwnd == IntPtr.Zero) ? hwndParent : msg1.hwnd;
                                System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT();
                                point1.x = System.Windows.Forms.NativeMethods.Util.LOWORD(msg1.lParam);
                                point1.y = System.Windows.Forms.NativeMethods.Util.HIWORD(msg1.lParam);
                                System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, ptr1), new HandleRef(this.control, this.control.Handle), point1, 1);
                                Control control2 = control1.GetChildAtPoint(new Point(point1.x, point1.y));
                                if ((control2 != null) && (control2 != control1))
                                {
                                    System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(new HandleRef(control1, control1.Handle), new HandleRef(control2, control2.Handle), point1, 1);
                                    control1 = control2;
                                }
                                msg1.lParam = System.Windows.Forms.NativeMethods.Util.MAKELPARAM(point1.x, point1.y);
                            }
                            if ((msg1.message == 0x100) && (msg1.wParam == ((IntPtr) 9)))
                            {
                                control1.SelectNextControl(null, Control.ModifierKeys != Keys.Shift, true, true, true);
                                return;
                            }
                            control1.SendMessage(msg1.message, msg1.wParam, msg1.lParam);
                        }
                        return;
                    }
                    case -3:
                    {
                        this.UIDeactivate();
                        this.InPlaceDeactivate();
                        if (this.activeXState[Control.ActiveXImpl.inPlaceVisible])
                        {
                            this.SetInPlaceVisible(false);
                        }
                        return;
                    }
                }
                Control.ActiveXImpl.ThrowHr(-2147467263);
            }

            internal void Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, System.Windows.Forms.NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, System.Windows.Forms.NativeMethods.COMRECT prcBounds, System.Windows.Forms.NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
            {
                int num3 = dwDrawAspect;
                if (((num3 != 1) && (num3 != 0x10)) && (num3 != 0x20))
                {
                    Control.ActiveXImpl.ThrowHr(-2147221397);
                }
                int num1 = System.Windows.Forms.UnsafeNativeMethods.GetObjectType(new HandleRef(null, hdcDraw));
                if (num1 == 4)
                {
                    Control.ActiveXImpl.ThrowHr(-2147221184);
                }
                System.Windows.Forms.NativeMethods.POINT point1 = new System.Windows.Forms.NativeMethods.POINT();
                System.Windows.Forms.NativeMethods.POINT point2 = new System.Windows.Forms.NativeMethods.POINT();
                System.Windows.Forms.NativeMethods.SIZE size1 = new System.Windows.Forms.NativeMethods.SIZE();
                System.Windows.Forms.NativeMethods.SIZE size2 = new System.Windows.Forms.NativeMethods.SIZE();
                int num2 = 1;
                if (!this.control.IsHandleCreated)
                {
                    this.control.CreateHandle();
                }
                if (prcBounds != null)
                {
                    System.Windows.Forms.NativeMethods.RECT rect1 = new System.Windows.Forms.NativeMethods.RECT(prcBounds.left, prcBounds.top, prcBounds.right, prcBounds.bottom);
                    System.Windows.Forms.SafeNativeMethods.LPtoDP(new HandleRef(null, hdcDraw), out rect1, 2);
                    num2 = System.Windows.Forms.SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), 8);
                    System.Windows.Forms.SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), 0, 0, point2);
                    System.Windows.Forms.SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), this.control.Width, this.control.Height, size1);
                    System.Windows.Forms.SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), rect1.left, rect1.top, point1);
                    System.Windows.Forms.SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), rect1.right - rect1.left, rect1.bottom - rect1.top, size2);
                }
                try
                {
                    IntPtr ptr1 = (IntPtr) 30;
                    if (num1 != 12)
                    {
                        this.control.SendMessage(0x317, hdcDraw, ptr1);
                    }
                    else
                    {
                        this.control.PrintToMetaFile(new HandleRef(null, hdcDraw), ptr1);
                    }
                }
                finally
                {
                    if (prcBounds != null)
                    {
                        System.Windows.Forms.SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), point2.x, point2.y, null);
                        System.Windows.Forms.SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), size1.cx, size1.cy, null);
                        System.Windows.Forms.SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), point1.x, point1.y, null);
                        System.Windows.Forms.SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), size2.cx, size2.cy, null);
                        System.Windows.Forms.SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), num2);
                    }
                }
            }

            internal static int EnumVerbs(out System.Windows.Forms.UnsafeNativeMethods.IEnumOLEVERB e)
            {
                if (Control.ActiveXImpl.axVerbs == null)
                {
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb1 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb2 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb3 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb4 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb5 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    System.Windows.Forms.NativeMethods.tagOLEVERB goleverb6 = new System.Windows.Forms.NativeMethods.tagOLEVERB();
                    goleverb1.lVerb = -1;
                    goleverb2.lVerb = -5;
                    goleverb3.lVerb = -4;
                    goleverb4.lVerb = -3;
                    goleverb5.lVerb = 0;
                    goleverb6.lVerb = -7;
                    goleverb6.lpszVerbName = System.Windows.Forms.SR.GetString("AXProperties");
                    goleverb6.grfAttribs = 2;
                    System.Windows.Forms.NativeMethods.tagOLEVERB[] goleverbArray1 = new System.Windows.Forms.NativeMethods.tagOLEVERB[5] { goleverb1, goleverb2, goleverb3, goleverb4, goleverb5 } ;
                    Control.ActiveXImpl.axVerbs = goleverbArray1;
                }
                e = new Control.ActiveXVerbEnum(Control.ActiveXImpl.axVerbs);
                return 0;
            }

            private static byte[] FromBase64WrappedString(string text)
            {
                if (text.IndexOfAny(new char[3] { ' ', '\r', '\n' } ) == -1)
                {
                    return Convert.FromBase64String(text);
                }
                StringBuilder builder1 = new StringBuilder(text.Length);
                for (int num1 = 0; num1 < text.Length; num1++)
                {
                    char ch1 = text[num1];
                    if (((ch1 != '\n') && (ch1 != '\r')) && (ch1 != ' '))
                    {
                        builder1.Append(text[num1]);
                    }
                }
                return Convert.FromBase64String(builder1.ToString());
            }

            internal void GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
            {
                if (paspects != null)
                {
                    paspects[0] = 1;
                }
                if (padvf != null)
                {
                    padvf[0] = 0;
                    if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
                    {
                        padvf[0] = padvf[0] | 4;
                    }
                    if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
                    {
                        padvf[0] = padvf[0] | 2;
                    }
                }
                if (pAdvSink != null)
                {
                    pAdvSink[0] = this.viewAdviseSink;
                }
            }

            private bool GetAmbientProperty(int dispid, ref object obj)
            {
                if (this.clientSite is System.Windows.Forms.UnsafeNativeMethods.IDispatch)
                {
                    System.Windows.Forms.UnsafeNativeMethods.IDispatch dispatch1 = (System.Windows.Forms.UnsafeNativeMethods.IDispatch) this.clientSite;
                    object[] objArray1 = new object[1];
                    Guid guid1 = Guid.Empty;
                    int num1 = -2147467259;
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        num1 = dispatch1.Invoke(dispid, ref guid1, System.Windows.Forms.NativeMethods.LOCALE_USER_DEFAULT, 2, new System.Windows.Forms.NativeMethods.tagDISPPARAMS(), objArray1, null, null);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    if (System.Windows.Forms.NativeMethods.Succeeded(num1))
                    {
                        obj = objArray1[0];
                        return true;
                    }
                }
                return false;
            }

            internal System.Windows.Forms.UnsafeNativeMethods.IOleClientSite GetClientSite()
            {
                return this.clientSite;
            }

            [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
            internal int GetControlInfo(System.Windows.Forms.NativeMethods.tagCONTROLINFO pCI)
            {
                if (this.accelCount == -1)
                {
                    ArrayList list1 = new ArrayList();
                    this.GetMnemonicList(this.control, list1);
                    this.accelCount = (short) list1.Count;
                    if (this.accelCount > 0)
                    {
                        int num1 = System.Windows.Forms.UnsafeNativeMethods.SizeOf(typeof(System.Windows.Forms.NativeMethods.ACCEL));
                        IntPtr ptr1 = Marshal.AllocHGlobal((int) ((num1 * this.accelCount) * 2));
                        try
                        {
                            System.Windows.Forms.NativeMethods.ACCEL accel1 = new System.Windows.Forms.NativeMethods.ACCEL();
                            accel1.cmd = 0;
                            this.accelCount = 0;
                            foreach (char ch1 in list1)
                            {
                                IntPtr ptr2 = (IntPtr) (((long) ptr1) + (this.accelCount * num1));
                                if ((ch1 >= 'A') && (ch1 <= 'Z'))
                                {
                                    accel1.fVirt = 0x11;
                                    accel1.key = (short) (System.Windows.Forms.UnsafeNativeMethods.VkKeyScan(ch1) & 0xff);
                                    Marshal.StructureToPtr(accel1, ptr2, false);
                                    this.accelCount = (short) (this.accelCount + 1);
                                    ptr2 = (IntPtr) (((long) ptr2) + num1);
                                    accel1.fVirt = 0x15;
                                    Marshal.StructureToPtr(accel1, ptr2, false);
                                }
                                else
                                {
                                    accel1.fVirt = 0x11;
                                    short num2 = System.Windows.Forms.UnsafeNativeMethods.VkKeyScan(ch1);
                                    if ((num2 & 0x100) != 0)
                                    {
                                        accel1.fVirt = (byte) (accel1.fVirt | 4);
                                    }
                                    accel1.key = (short) (num2 & 0xff);
                                    Marshal.StructureToPtr(accel1, ptr2, false);
                                }
                                accel1.cmd = (short) (accel1.cmd + 1);
                                this.accelCount = (short) (this.accelCount + 1);
                            }
                            this.accelTable = System.Windows.Forms.UnsafeNativeMethods.CreateAcceleratorTable(new HandleRef(null, ptr1), this.accelCount);
                        }
                        finally
                        {
                            if (ptr1 != IntPtr.Zero)
                            {
                                Marshal.FreeHGlobal(ptr1);
                            }
                        }
                    }
                }
                pCI.cAccel = this.accelCount;
                pCI.hAccel = this.accelTable;
                return 0;
            }

            internal void GetExtent(int dwDrawAspect, System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
            {
                if ((dwDrawAspect & 1) != 0)
                {
                    Size size1 = this.control.Size;
                    Point point1 = this.PixelToHiMetric(size1.Width, size1.Height);
                    pSizel.cx = point1.X;
                    pSizel.cy = point1.Y;
                }
                else
                {
                    Control.ActiveXImpl.ThrowHr(-2147221397);
                }
            }

            private void GetMnemonicList(Control control, ArrayList mnemonicList)
            {
                char ch1 = WindowsFormsUtils.GetMnemonic(control.Text, true);
                if (ch1 != '\0')
                {
                    mnemonicList.Add(ch1);
                }
                foreach (Control control1 in control.Controls)
                {
                    if (control1 != null)
                    {
                        this.GetMnemonicList(control1, mnemonicList);
                    }
                }
            }

            private string GetStreamName()
            {
                string text1 = this.control.GetType().FullName;
                int num1 = text1.Length;
                if (num1 > 0x1f)
                {
                    text1 = text1.Substring(num1 - 0x1f);
                }
                return text1;
            }

            internal int GetWindow(out IntPtr hwnd)
            {
                if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
                {
                    hwnd = IntPtr.Zero;
                    return -2147467259;
                }
                hwnd = this.control.Handle;
                return 0;
            }

            private Point HiMetricToPixel(int x, int y)
            {
                Point point1 = new Point();
                point1.X = ((this.LogPixels.X * x) + (Control.ActiveXImpl.hiMetricPerInch / 2)) / Control.ActiveXImpl.hiMetricPerInch;
                point1.Y = ((this.LogPixels.Y * y) + (Control.ActiveXImpl.hiMetricPerInch / 2)) / Control.ActiveXImpl.hiMetricPerInch;
                return point1;
            }

            [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
            internal void InPlaceActivate(int verb)
            {
                System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite;
                if (site1 != null)
                {
                    if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
                    {
                        int num1 = site1.CanInPlaceActivate();
                        if (num1 != 0)
                        {
                            if (System.Windows.Forms.NativeMethods.Succeeded(num1))
                            {
                                num1 = -2147467259;
                            }
                            Control.ActiveXImpl.ThrowHr(num1);
                        }
                        site1.OnInPlaceActivate();
                        this.activeXState[Control.ActiveXImpl.inPlaceActive] = true;
                    }
                    if (!this.activeXState[Control.ActiveXImpl.inPlaceVisible])
                    {
                        System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceFrame frame1;
                        System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceUIWindow window1;
                        System.Windows.Forms.NativeMethods.tagOIFI goifi1 = new System.Windows.Forms.NativeMethods.tagOIFI();
                        goifi1.cb = System.Windows.Forms.UnsafeNativeMethods.SizeOf(typeof(System.Windows.Forms.NativeMethods.tagOIFI));
                        IntPtr ptr1 = IntPtr.Zero;
                        ptr1 = site1.GetWindow();
                        System.Windows.Forms.NativeMethods.COMRECT comrect1 = new System.Windows.Forms.NativeMethods.COMRECT();
                        System.Windows.Forms.NativeMethods.COMRECT comrect2 = new System.Windows.Forms.NativeMethods.COMRECT();
                        if ((this.inPlaceUiWindow != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
                        {
                            System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
                            this.inPlaceUiWindow = null;
                        }
                        if ((this.inPlaceFrame != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
                        {
                            System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
                            this.inPlaceFrame = null;
                        }
                        site1.GetWindowContext(out frame1, out window1, comrect1, comrect2, goifi1);
                        this.SetObjectRects(comrect1, comrect2);
                        this.inPlaceFrame = frame1;
                        this.inPlaceUiWindow = window1;
                        this.hwndParent = ptr1;
                        System.Windows.Forms.UnsafeNativeMethods.SetParent(new HandleRef(this.control, this.control.Handle), new HandleRef(null, ptr1));
                        this.control.CreateControl();
                        this.clientSite.ShowObject();
                        this.SetInPlaceVisible(true);
                    }
                    if (((verb == 0) || (verb == -4)) && !this.activeXState[Control.ActiveXImpl.uiActive])
                    {
                        this.activeXState[Control.ActiveXImpl.uiActive] = true;
                        site1.OnUIActivate();
                        if (!this.control.ContainsFocus)
                        {
                            this.control.FocusInternal();
                        }
                        this.inPlaceFrame.SetActiveObject(this.control, null);
                        if (this.inPlaceUiWindow != null)
                        {
                            this.inPlaceUiWindow.SetActiveObject(this.control, null);
                        }
                        int num2 = this.inPlaceFrame.SetBorderSpace(null);
                        if ((System.Windows.Forms.NativeMethods.Failed(num2) && (num2 != -2147221087)) && (num2 != -2147467263))
                        {
                            System.Windows.Forms.UnsafeNativeMethods.ThrowExceptionForHR(num2);
                        }
                        if (this.inPlaceUiWindow != null)
                        {
                            num2 = this.inPlaceFrame.SetBorderSpace(null);
                            if ((System.Windows.Forms.NativeMethods.Failed(num2) && (num2 != -2147221087)) && (num2 != -2147467263))
                            {
                                System.Windows.Forms.UnsafeNativeMethods.ThrowExceptionForHR(num2);
                            }
                        }
                    }
                }
            }

            internal void InPlaceDeactivate()
            {
                if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
                {
                    if (this.activeXState[Control.ActiveXImpl.uiActive])
                    {
                        this.UIDeactivate();
                    }
                    this.activeXState[Control.ActiveXImpl.inPlaceActive] = false;
                    this.activeXState[Control.ActiveXImpl.inPlaceVisible] = false;
                    System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite;
                    if (site1 != null)
                    {
                        System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                        try
                        {
                            site1.OnInPlaceDeactivate();
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                    }
                    this.control.Visible = false;
                    this.hwndParent = IntPtr.Zero;
                    if ((this.inPlaceUiWindow != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
                    {
                        System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
                        this.inPlaceUiWindow = null;
                    }
                    if ((this.inPlaceFrame != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
                    {
                        System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
                        this.inPlaceFrame = null;
                    }
                }
            }

            internal int IsDirty()
            {
                if (this.activeXState[Control.ActiveXImpl.isDirty])
                {
                    return 0;
                }
                return 1;
            }

            private bool IsResourceProp(PropertyDescriptor prop)
            {
                TypeConverter converter1 = prop.Converter;
                System.Type[] typeArray2 = new System.Type[2] { typeof(string), typeof(byte[]) } ;
                System.Type[] typeArray1 = typeArray2;
                System.Type[] typeArray3 = typeArray1;
                for (int num1 = 0; num1 < typeArray3.Length; num1++)
                {
                    System.Type type1 = typeArray3[num1];
                    if (converter1.CanConvertTo(type1) && converter1.CanConvertFrom(type1))
                    {
                        return false;
                    }
                }
                return (prop.GetValue(this.control) is ISerializable);
            }

            internal void Load(System.Windows.Forms.UnsafeNativeMethods.IStorage stg)
            {
                System.Windows.Forms.UnsafeNativeMethods.IStream stream1 = null;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    stream1 = stg.OpenStream(this.GetStreamName(), IntPtr.Zero, 0x10, 0);
                }
                catch (COMException exception1)
                {
                    if (exception1.ErrorCode != -2147287038)
                    {
                        throw;
                    }
                    stream1 = stg.OpenStream(base.GetType().FullName, IntPtr.Zero, 0x10, 0);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                this.Load(stream1);
                stream1 = null;
                if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(stg))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(stg);
                }
            }

            internal void Load(System.Windows.Forms.UnsafeNativeMethods.IStream stream)
            {
                System.Windows.Forms.Control.ActiveXImpl.PropertyBagStream stream1 = new System.Windows.Forms.Control.ActiveXImpl.PropertyBagStream();
                stream1.Read(stream);
                this.Load(stream1, null);
                if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(stream))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(stream);
                }
            }

            internal void Load(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag pPropBag, System.Windows.Forms.UnsafeNativeMethods.IErrorLog pErrorLog)
            {
                Attribute[] attributeArray1 = new Attribute[1] { DesignerSerializationVisibilityAttribute.Visible } ;
                PropertyDescriptorCollection collection1 = TypeDescriptor.GetProperties(this.control, attributeArray1);
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    try
                    {
                        object obj1 = null;
                        int num2 = -2147467259;
                        System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                        try
                        {
                            num2 = pPropBag.Read(collection1[num1].Name, out obj1, pErrorLog);
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                        if (System.Windows.Forms.NativeMethods.Succeeded(num2) && (obj1 != null))
                        {
                            string text1 = null;
                            int num3 = 0;
                            try
                            {
                                if (obj1.GetType() != typeof(string))
                                {
                                    obj1 = Convert.ToString(obj1, CultureInfo.InvariantCulture);
                                }
                                if (this.IsResourceProp(collection1[num1]))
                                {
                                    byte[] buffer1 = Convert.FromBase64String(obj1.ToString());
                                    MemoryStream stream1 = new MemoryStream(buffer1);
                                    BinaryFormatter formatter1 = new BinaryFormatter();
                                    collection1[num1].SetValue(this.control, formatter1.Deserialize(stream1));
                                }
                                else
                                {
                                    TypeConverter converter1 = collection1[num1].Converter;
                                    object obj2 = null;
                                    if (converter1.CanConvertFrom(typeof(string)))
                                    {
                                        obj2 = converter1.ConvertFromInvariantString(obj1.ToString());
                                    }
                                    else if (converter1.CanConvertFrom(typeof(byte[])))
                                    {
                                        string text2 = obj1.ToString();
                                        obj2 = converter1.ConvertFrom(null, CultureInfo.InvariantCulture, Control.ActiveXImpl.FromBase64WrappedString(text2));
                                    }
                                    collection1[num1].SetValue(this.control, obj2);
                                }
                            }
                            catch (Exception exception1)
                            {
                                text1 = exception1.ToString();
                                if (exception1 is ExternalException)
                                {
                                    num3 = ((ExternalException) exception1).ErrorCode;
                                }
                                else
                                {
                                    num3 = -2147467259;
                                }
                            }
                            catch
                            {
                                text1 = System.Windows.Forms.SR.GetString("AXUnknownError");
                                num3 = -2147467259;
                            }
                            if ((text1 != null) && (pErrorLog != null))
                            {
                                System.Windows.Forms.NativeMethods.tagEXCEPINFO gexcepinfo1 = new System.Windows.Forms.NativeMethods.tagEXCEPINFO();
                                gexcepinfo1.bstrSource = this.control.GetType().FullName;
                                gexcepinfo1.bstrDescription = text1;
                                gexcepinfo1.scode = num3;
                                pErrorLog.AddError(collection1[num1].Name, gexcepinfo1);
                            }
                        }
                    }
                    catch (Exception exception2)
                    {
                        if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception2))
                        {
                            throw;
                        }
                    }
                    catch
                    {
                    }
                }
                if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(pPropBag))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(pPropBag);
                }
            }

            private Control.AmbientProperty LookupAmbient(int dispid)
            {
                for (int num1 = 0; num1 < this.ambientProperties.Length; num1++)
                {
                    if (this.ambientProperties[num1].DispID == dispid)
                    {
                        return this.ambientProperties[num1];
                    }
                }
                return this.ambientProperties[0];
            }

            internal IntPtr MergeRegion(IntPtr region)
            {
                IntPtr ptr2;
                if (this.clipRegion == IntPtr.Zero)
                {
                    return region;
                }
                if (region == IntPtr.Zero)
                {
                    return this.clipRegion;
                }
                try
                {
                    IntPtr ptr1 = System.Windows.Forms.SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
                    try
                    {
                        System.Windows.Forms.SafeNativeMethods.CombineRgn(new HandleRef(null, ptr1), new HandleRef(null, region), new HandleRef(this, this.clipRegion), 4);
                        System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(null, region));
                    }
                    catch
                    {
                        System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(null, ptr1));
                        throw;
                    }
                    ptr2 = ptr1;
                }
                catch (Exception exception1)
                {
                    if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                    {
                        throw;
                    }
                    ptr2 = region;
                }
                catch
                {
                    ptr2 = region;
                }
                return ptr2;
            }

            internal void OnAmbientPropertyChange(int dispID)
            {
                if (dispID != -1)
                {
                    for (int num1 = 0; num1 < this.ambientProperties.Length; num1++)
                    {
                        if (this.ambientProperties[num1].DispID == dispID)
                        {
                            this.ambientProperties[num1].ResetValue();
                            this.CallParentPropertyChanged(this.control, this.ambientProperties[num1].Name);
                            return;
                        }
                    }
                    object obj1 = new object();
                    int num3 = dispID;
                    if (num3 != -713)
                    {
                        if (num3 == -710)
                        {
                            if (!this.GetAmbientProperty(-710, ref obj1))
                            {
                                return;
                            }
                            this.activeXState[Control.ActiveXImpl.uiDead] = (bool) obj1;
                        }
                    }
                    else
                    {
                        IButtonControl control1 = this.control as IButtonControl;
                        if ((control1 != null) && this.GetAmbientProperty(-713, ref obj1))
                        {
                            control1.NotifyDefault((bool) obj1);
                        }
                    }
                }
                else
                {
                    for (int num2 = 0; num2 < this.ambientProperties.Length; num2++)
                    {
                        this.ambientProperties[num2].ResetValue();
                        this.CallParentPropertyChanged(this.control, this.ambientProperties[num2].Name);
                    }
                }
            }

            internal void OnDocWindowActivate(int fActivate)
            {
                if ((this.activeXState[Control.ActiveXImpl.uiActive] && (fActivate != 0)) && (this.inPlaceFrame != null))
                {
                    int num1;
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        num1 = this.inPlaceFrame.SetBorderSpace(null);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    if ((System.Windows.Forms.NativeMethods.Failed(num1) && (num1 != -2147221087)) && (num1 != -2147467263))
                    {
                        System.Windows.Forms.UnsafeNativeMethods.ThrowExceptionForHR(num1);
                    }
                }
            }

            internal void OnFocus(bool focus)
            {
                if (this.activeXState[Control.ActiveXImpl.inPlaceActive] && (this.clientSite is System.Windows.Forms.UnsafeNativeMethods.IOleControlSite))
                {
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        ((System.Windows.Forms.UnsafeNativeMethods.IOleControlSite) this.clientSite).OnFocus(focus ? 1 : 0);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                }
                if ((focus && this.activeXState[Control.ActiveXImpl.inPlaceActive]) && !this.activeXState[Control.ActiveXImpl.uiActive])
                {
                    this.InPlaceActivate(-4);
                }
            }

            private Point PixelToHiMetric(int x, int y)
            {
                Point point1 = new Point();
                point1.X = ((Control.ActiveXImpl.hiMetricPerInch * x) + (this.LogPixels.X >> 1)) / this.LogPixels.X;
                point1.Y = ((Control.ActiveXImpl.hiMetricPerInch * y) + (this.LogPixels.Y >> 1)) / this.LogPixels.Y;
                return point1;
            }

            internal void QuickActivate(System.Windows.Forms.UnsafeNativeMethods.tagQACONTAINER pQaContainer, System.Windows.Forms.UnsafeNativeMethods.tagQACONTROL pQaControl)
            {
                int num1;
                Control.AmbientProperty property1 = this.LookupAmbient(-701);
                property1.Value = ColorTranslator.FromOle((int) pQaContainer.colorBack);
                property1 = this.LookupAmbient(-704);
                property1.Value = ColorTranslator.FromOle((int) pQaContainer.colorFore);
                if (pQaContainer.pFont != null)
                {
                    property1 = this.LookupAmbient(-703);
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        IntPtr ptr1 = IntPtr.Zero;
                        System.Windows.Forms.UnsafeNativeMethods.IFont font1 = (System.Windows.Forms.UnsafeNativeMethods.IFont) pQaContainer.pFont;
                        ptr1 = font1.GetHFont();
                        Font font2 = Font.FromHfont(ptr1);
                        property1.Value = font2;
                    }
                    catch (Exception exception1)
                    {
                        if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                        {
                            throw;
                        }
                        property1.Value = null;
                    }
                    catch
                    {
                        property1.Value = null;
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                }
                pQaControl.cbSize = System.Windows.Forms.UnsafeNativeMethods.SizeOf(typeof(System.Windows.Forms.UnsafeNativeMethods.tagQACONTROL));
                this.SetClientSite(pQaContainer.pClientSite);
                if (pQaContainer.pAdviseSink != null)
                {
                    this.SetAdvise(1, 0, (IAdviseSink) pQaContainer.pAdviseSink);
                }
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    ((System.Windows.Forms.UnsafeNativeMethods.IOleObject) this.control).GetMiscStatus(1, out num1);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                pQaControl.dwMiscStatus = num1;
            }

            internal void Save(System.Windows.Forms.UnsafeNativeMethods.IStorage stg, bool fSameAsLoad)
            {
                System.Windows.Forms.UnsafeNativeMethods.IStream stream1 = null;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    stream1 = stg.CreateStream(this.GetStreamName(), 0x1011, 0, 0);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                this.Save(stream1, true);
                System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(stream1);
            }

            internal void Save(System.Windows.Forms.UnsafeNativeMethods.IStream stream, bool fClearDirty)
            {
                System.Windows.Forms.Control.ActiveXImpl.PropertyBagStream stream1 = new System.Windows.Forms.Control.ActiveXImpl.PropertyBagStream();
                this.Save(stream1, fClearDirty, false);
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    stream1.Write(stream);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(stream))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(stream);
                }
            }

            [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
            internal void Save(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
            {
                Attribute[] attributeArray1 = new Attribute[1] { DesignerSerializationVisibilityAttribute.Visible } ;
                PropertyDescriptorCollection collection1 = TypeDescriptor.GetProperties(this.control, attributeArray1);
                for (int num1 = 0; num1 < collection1.Count; num1++)
                {
                    if (fSaveAllProperties || collection1[num1].ShouldSerializeValue(this.control))
                    {
                        object obj1;
                        if (this.IsResourceProp(collection1[num1]))
                        {
                            MemoryStream stream1 = new MemoryStream();
                            BinaryFormatter formatter1 = new BinaryFormatter();
                            formatter1.Serialize(stream1, collection1[num1].GetValue(this.control));
                            byte[] buffer1 = new byte[(int) stream1.Length];
                            stream1.Position = 0;
                            stream1.Read(buffer1, 0, buffer1.Length);
                            obj1 = Convert.ToBase64String(buffer1);
                            pPropBag.Write(collection1[num1].Name, ref obj1);
                        }
                        else
                        {
                            TypeConverter converter1 = collection1[num1].Converter;
                            if (converter1.CanConvertFrom(typeof(string)))
                            {
                                obj1 = converter1.ConvertToInvariantString(collection1[num1].GetValue(this.control));
                                pPropBag.Write(collection1[num1].Name, ref obj1);
                            }
                            else if (converter1.CanConvertFrom(typeof(byte[])))
                            {
                                byte[] buffer2 = (byte[]) converter1.ConvertTo(null, CultureInfo.InvariantCulture, collection1[num1].GetValue(this.control), typeof(byte[]));
                                obj1 = Convert.ToBase64String(buffer2);
                                pPropBag.Write(collection1[num1].Name, ref obj1);
                            }
                        }
                    }
                }
                if (fClearDirty)
                {
                    this.activeXState[Control.ActiveXImpl.isDirty] = false;
                }
            }

            private void SendOnSave()
            {
                int num1 = this.adviseList.Count;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                for (int num2 = 0; num2 < num1; num2++)
                {
                    IAdviseSink sink1 = (IAdviseSink) this.adviseList[num2];
                    sink1.OnSave();
                }
            }

            internal void SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
            {
                if ((aspects & 1) == 0)
                {
                    Control.ActiveXImpl.ThrowHr(-2147221397);
                }
                this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst] = ((advf & 2) != 0) || false;
                this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce] = ((advf & 4) != 0) || false;
                if ((this.viewAdviseSink != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
                }
                this.viewAdviseSink = pAdvSink;
                if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
                {
                    this.ViewChanged();
                }
            }

            internal void SetClientSite(System.Windows.Forms.UnsafeNativeMethods.IOleClientSite value)
            {
                if (this.clientSite != null)
                {
                    if (value == null)
                    {
                        Control.ActiveXImpl.globalActiveXCount--;
                        if ((Control.ActiveXImpl.globalActiveXCount == 0) && this.IsIE)
                        {
                            new PermissionSet(PermissionState.Unrestricted).Assert();
                            try
                            {
                                MethodInfo info1 = typeof(SystemEvents).GetMethod("Shutdown", BindingFlags.InvokeMethod | (BindingFlags.NonPublic | BindingFlags.Static), null, new System.Type[0], new ParameterModifier[0]);
                                if (info1 != null)
                                {
                                    info1.Invoke(null, null);
                                }
                            }
                            finally
                            {
                                CodeAccessPermission.RevertAssert();
                            }
                        }
                    }
                    if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.clientSite))
                    {
                        System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                        try
                        {
                            Marshal.FinalReleaseComObject(this.clientSite);
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                    }
                }
                this.clientSite = value;
                if (this.clientSite != null)
                {
                    this.control.Site = new Control.AxSourcingSite(this.control, this.clientSite, "ControlAxSourcingSite");
                }
                else
                {
                    this.control.Site = null;
                }
                object obj1 = new object();
                if (this.GetAmbientProperty(-710, ref obj1))
                {
                    this.activeXState[Control.ActiveXImpl.uiDead] = (bool) obj1;
                }
                if ((this.control is IButtonControl) && this.GetAmbientProperty(-710, ref obj1))
                {
                    ((IButtonControl) this.control).NotifyDefault((bool) obj1);
                }
                if (this.clientSite == null)
                {
                    if (this.accelTable != IntPtr.Zero)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.DestroyAcceleratorTable(new HandleRef(this, this.accelTable));
                        this.accelTable = IntPtr.Zero;
                        this.accelCount = -1;
                    }
                    if (this.IsIE)
                    {
                        this.control.Dispose();
                    }
                }
                else
                {
                    Control.ActiveXImpl.globalActiveXCount++;
                    if ((Control.ActiveXImpl.globalActiveXCount == 1) && this.IsIE)
                    {
                        new PermissionSet(PermissionState.Unrestricted).Assert();
                        try
                        {
                            MethodInfo info2 = typeof(SystemEvents).GetMethod("Startup", BindingFlags.InvokeMethod | (BindingFlags.NonPublic | BindingFlags.Static), null, new System.Type[0], new ParameterModifier[0]);
                            if (info2 != null)
                            {
                                info2.Invoke(null, null);
                            }
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                    }
                }
                this.control.OnTopMostActiveXParentChanged(EventArgs.Empty);
            }

            internal void SetExtent(int dwDrawAspect, System.Windows.Forms.NativeMethods.tagSIZEL pSizel)
            {
                if ((dwDrawAspect & 1) != 0)
                {
                    if (this.activeXState[Control.ActiveXImpl.changingExtents])
                    {
                        return;
                    }
                    this.activeXState[Control.ActiveXImpl.changingExtents] = true;
                    try
                    {
                        Size size1 = new Size(this.HiMetricToPixel(pSizel.cx, pSizel.cy));
                        if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
                        {
                            System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite;
                            if (site1 != null)
                            {
                                Rectangle rectangle1 = this.control.Bounds;
                                rectangle1.Location = new Point(rectangle1.X, rectangle1.Y);
                                Size size2 = new Size(size1.Width, size1.Height);
                                rectangle1.Width = size2.Width;
                                rectangle1.Height = size2.Height;
                                site1.OnPosRectChange(System.Windows.Forms.NativeMethods.COMRECT.FromXYWH(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height));
                            }
                        }
                        this.control.Size = size1;
                        if (!this.control.Size.Equals(size1))
                        {
                            this.activeXState[Control.ActiveXImpl.isDirty] = true;
                            if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
                            {
                                this.ViewChanged();
                            }
                            if (this.activeXState[Control.ActiveXImpl.inPlaceActive] || (this.clientSite == null))
                            {
                                return;
                            }
                            System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                            try
                            {
                                this.clientSite.RequestNewObjectLayout();
                                return;
                            }
                            finally
                            {
                                CodeAccessPermission.RevertAssert();
                            }
                        }
                        return;
                    }
                    finally
                    {
                        this.activeXState[Control.ActiveXImpl.changingExtents] = false;
                    }
                }
                Control.ActiveXImpl.ThrowHr(-2147221397);
            }

            private void SetInPlaceVisible(bool visible)
            {
                this.activeXState[Control.ActiveXImpl.inPlaceVisible] = visible;
                this.control.Visible = visible;
            }

            internal void SetObjectRects(System.Windows.Forms.NativeMethods.COMRECT lprcPosRect, System.Windows.Forms.NativeMethods.COMRECT lprcClipRect)
            {
                Rectangle rectangle1 = Rectangle.FromLTRB(lprcPosRect.left, lprcPosRect.top, lprcPosRect.right, lprcPosRect.bottom);
                if (this.activeXState[Control.ActiveXImpl.adjustingRect])
                {
                    this.adjustRect.left = rectangle1.X;
                    this.adjustRect.top = rectangle1.Y;
                    this.adjustRect.right = rectangle1.Width + rectangle1.X;
                    this.adjustRect.bottom = rectangle1.Height + rectangle1.Y;
                }
                else
                {
                    this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
                    try
                    {
                        this.control.Bounds = rectangle1;
                    }
                    finally
                    {
                        this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
                    }
                }
                bool flag1 = false;
                if (this.clipRegion != IntPtr.Zero)
                {
                    this.clipRegion = IntPtr.Zero;
                    flag1 = true;
                }
                if (lprcClipRect != null)
                {
                    Rectangle rectangle3;
                    Rectangle rectangle2 = Rectangle.FromLTRB(lprcClipRect.left, lprcClipRect.top, lprcClipRect.right, lprcClipRect.bottom);
                    if (!rectangle2.IsEmpty)
                    {
                        rectangle3 = Rectangle.Intersect(rectangle1, rectangle2);
                    }
                    else
                    {
                        rectangle3 = rectangle1;
                    }
                    if (!rectangle3.Equals(rectangle1))
                    {
                        System.Windows.Forms.NativeMethods.RECT rect1 = System.Windows.Forms.NativeMethods.RECT.FromXYWH(rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height);
                        IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetParent(new HandleRef(this.control, this.control.Handle));
                        System.Windows.Forms.UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, ptr1), new HandleRef(this.control, this.control.Handle), out rect1, 2);
                        this.clipRegion = System.Windows.Forms.SafeNativeMethods.CreateRectRgn(rect1.left, rect1.top, rect1.right, rect1.bottom);
                        flag1 = true;
                    }
                }
                if (flag1 && this.control.IsHandleCreated)
                {
                    IntPtr ptr2 = this.clipRegion;
                    Region region1 = this.control.Region;
                    if (region1 != null)
                    {
                        IntPtr ptr3 = this.control.GetHRgn(region1);
                        ptr2 = this.MergeRegion(ptr3);
                    }
                    System.Windows.Forms.UnsafeNativeMethods.SetWindowRgn(new HandleRef(this.control, this.control.Handle), new HandleRef(this, ptr2), System.Windows.Forms.SafeNativeMethods.IsWindowVisible(new HandleRef(this.control, this.control.Handle)));
                }
                this.control.Invalidate();
            }

            void IWindowTarget.OnHandleChange(IntPtr newHandle)
            {
                this.controlWindowTarget.OnHandleChange(newHandle);
            }

            void IWindowTarget.OnMessage(ref Message m)
            {
                if (this.activeXState[Control.ActiveXImpl.uiDead])
                {
                    if ((m.Msg >= 0x200) && (m.Msg <= 0x20a))
                    {
                        return;
                    }
                    if ((m.Msg >= 0xa1) && (m.Msg <= 0xa9))
                    {
                        return;
                    }
                    if ((m.Msg >= 0x100) && (m.Msg <= 0x108))
                    {
                        return;
                    }
                }
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                this.controlWindowTarget.OnMessage(ref m);
            }

            internal static void ThrowHr(int hr)
            {
                ExternalException exception1 = new ExternalException(System.Windows.Forms.SR.GetString("ExternalException"), hr);
                throw exception1;
            }

            internal int TranslateAccelerator(ref System.Windows.Forms.NativeMethods.MSG lpmsg)
            {
                int num1 = 1;
                bool flag1 = false;
                bool flag2 = true;
                switch (lpmsg.message)
                {
                    case 0x100:
                    case 0x102:
                    case 260:
                    case 0x106:
                    {
                        flag1 = true;
                        break;
                    }
                }
                Message message1 = Message.Create(lpmsg.hwnd, lpmsg.message, lpmsg.wParam, lpmsg.lParam);
                if (flag1)
                {
                    Control control1 = Control.FromChildHandleInternal(lpmsg.hwnd);
                    if ((control1 != null) && ((this.control == control1) || this.control.Contains(control1)))
                    {
                        if (control1.PreProcessMessage(ref message1))
                        {
                            lpmsg.message = message1.Msg;
                            lpmsg.wParam = message1.WParam;
                            lpmsg.lParam = message1.LParam;
                            num1 = 0;
                        }
                        else if ((message1.Msg == 0x100) || (message1.Msg == 260))
                        {
                            Keys keys1 = ((Keys) ((int) message1.WParam)) | Control.ModifierKeys;
                            if (control1.IsInputKey(keys1))
                            {
                                flag2 = false;
                            }
                        }
                        else if ((message1.Msg == 0x102) && control1.IsInputChar((char) ((ushort) ((int) message1.WParam))))
                        {
                            flag2 = false;
                        }
                    }
                }
                if ((num1 == 1) && flag2)
                {
                    System.Windows.Forms.UnsafeNativeMethods.IOleControlSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleControlSite;
                    if (site1 == null)
                    {
                        return num1;
                    }
                    int num2 = 0;
                    if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x10) < 0)
                    {
                        num2 |= 1;
                    }
                    if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x11) < 0)
                    {
                        num2 |= 2;
                    }
                    if (System.Windows.Forms.UnsafeNativeMethods.GetKeyState(0x12) < 0)
                    {
                        num2 |= 4;
                    }
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        num1 = site1.TranslateAccelerator(ref lpmsg, num2);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                }
                return num1;
            }

            internal int UIDeactivate()
            {
                if (this.activeXState[Control.ActiveXImpl.uiActive])
                {
                    this.activeXState[Control.ActiveXImpl.uiActive] = false;
                    if (this.inPlaceUiWindow != null)
                    {
                        this.inPlaceUiWindow.SetActiveObject(null, null);
                    }
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    this.inPlaceFrame.SetActiveObject(null, null);
                    System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite;
                    if (site1 != null)
                    {
                        site1.OnUIDeactivate(0);
                    }
                }
                return 0;
            }

            internal void Unadvise(int dwConnection)
            {
                if ((dwConnection > this.adviseList.Count) || (this.adviseList[dwConnection - 1] == null))
                {
                    Control.ActiveXImpl.ThrowHr(-2147221500);
                }
                IAdviseSink sink1 = (IAdviseSink) this.adviseList[dwConnection - 1];
                this.adviseList.RemoveAt(dwConnection - 1);
                if ((sink1 != null) && System.Windows.Forms.UnsafeNativeMethods.IsComObject(sink1))
                {
                    System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(sink1);
                }
            }

            internal void UpdateAccelTable()
            {
                this.accelCount = -1;
                System.Windows.Forms.UnsafeNativeMethods.IOleControlSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleControlSite;
                if (site1 != null)
                {
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    site1.OnControlInfoChanged();
                }
            }

            internal void UpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
            {
                if (!this.activeXState[Control.ActiveXImpl.adjustingRect] && this.activeXState[Control.ActiveXImpl.inPlaceVisible])
                {
                    System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite site1 = this.clientSite as System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceSite;
                    if (site1 != null)
                    {
                        System.Windows.Forms.NativeMethods.COMRECT comrect1 = new System.Windows.Forms.NativeMethods.COMRECT();
                        if ((flags & 2) != 0)
                        {
                            comrect1.left = this.control.Left;
                            comrect1.top = this.control.Top;
                        }
                        else
                        {
                            comrect1.left = x;
                            comrect1.top = y;
                        }
                        if ((flags & 1) != 0)
                        {
                            comrect1.right = comrect1.left + this.control.Width;
                            comrect1.bottom = comrect1.top + this.control.Height;
                        }
                        else
                        {
                            comrect1.right = comrect1.left + width;
                            comrect1.bottom = comrect1.top + height;
                        }
                        this.adjustRect = comrect1;
                        this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
                        System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                        try
                        {
                            site1.OnPosRectChange(comrect1);
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                            this.adjustRect = null;
                            this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
                        }
                        if ((flags & 2) == 0)
                        {
                            x = comrect1.left;
                            y = comrect1.top;
                        }
                        if ((flags & 1) == 0)
                        {
                            width = comrect1.right - comrect1.left;
                            height = comrect1.bottom - comrect1.top;
                        }
                    }
                }
            }

            private void ViewChanged()
            {
                if ((this.viewAdviseSink != null) && !this.activeXState[Control.ActiveXImpl.saving])
                {
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                    try
                    {
                        this.viewAdviseSink.OnViewChange(1, -1);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
                    {
                        if (System.Windows.Forms.UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
                        {
                            System.Windows.Forms.UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
                        }
                        this.viewAdviseSink = null;
                    }
                }
            }

            internal void ViewChangedInternal()
            {
                this.ViewChanged();
            }


            // Properties
            [EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
            internal System.Drawing.Color AmbientBackColor
            {
                get
                {
                    Control.AmbientProperty property1 = this.LookupAmbient(-701);
                    if (property1.Empty)
                    {
                        object obj1 = null;
                        if (this.GetAmbientProperty(-701, ref obj1) && (obj1 != null))
                        {
                            try
                            {
                                property1.Value = ColorTranslator.FromOle(Convert.ToInt32(obj1, CultureInfo.InvariantCulture));
                            }
                            catch (Exception exception1)
                            {
                                if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                                {
                                    throw;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    if (property1.Value == null)
                    {
                        return System.Drawing.Color.Empty;
                    }
                    return (System.Drawing.Color) property1.Value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            internal Font AmbientFont
            {
                get
                {
                    Control.AmbientProperty property1 = this.LookupAmbient(-703);
                    if (property1.Empty)
                    {
                        object obj1 = null;
                        if (this.GetAmbientProperty(-703, ref obj1))
                        {
                            try
                            {
                                IntPtr ptr1 = IntPtr.Zero;
                                System.Windows.Forms.UnsafeNativeMethods.IFont font1 = (System.Windows.Forms.UnsafeNativeMethods.IFont) obj1;
                                System.Windows.Forms.IntSecurity.ObjectFromWin32Handle.Assert();
                                Font font2 = null;
                                try
                                {
                                    ptr1 = font1.GetHFont();
                                    font2 = Font.FromHfont(ptr1);
                                }
                                finally
                                {
                                    CodeAccessPermission.RevertAssert();
                                }
                                property1.Value = font2;
                            }
                            catch (Exception exception1)
                            {
                                if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                                {
                                    throw;
                                }
                                property1.Value = null;
                            }
                            catch
                            {
                                property1.Value = null;
                            }
                        }
                    }
                    return (Font) property1.Value;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
            internal System.Drawing.Color AmbientForeColor
            {
                get
                {
                    Control.AmbientProperty property1 = this.LookupAmbient(-704);
                    if (property1.Empty)
                    {
                        object obj1 = null;
                        if (this.GetAmbientProperty(-704, ref obj1) && (obj1 != null))
                        {
                            try
                            {
                                property1.Value = ColorTranslator.FromOle(Convert.ToInt32(obj1, CultureInfo.InvariantCulture));
                            }
                            catch (Exception exception1)
                            {
                                if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                                {
                                    throw;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    if (property1.Value == null)
                    {
                        return System.Drawing.Color.Empty;
                    }
                    return (System.Drawing.Color) property1.Value;
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            internal bool EventsFrozen
            {
                get
                {
                    return this.activeXState[Control.ActiveXImpl.eventsFrozen];
                }
                set
                {
                    this.activeXState[Control.ActiveXImpl.eventsFrozen] = value;
                }
            }

            internal IntPtr HWNDParent
            {
                get
                {
                    return this.hwndParent;
                }
            }

            internal bool IsIE
            {
                [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    if (!Control.ActiveXImpl.checkedIE)
                    {
                        System.Windows.Forms.UnsafeNativeMethods.IOleContainer container1;
                        if (this.clientSite == null)
                        {
                            return false;
                        }
                        if (((Assembly.GetEntryAssembly() == null) && System.Windows.Forms.NativeMethods.Succeeded(this.clientSite.GetContainer(out container1))) && (container1 is System.Windows.Forms.NativeMethods.IHTMLDocument))
                        {
                            Control.ActiveXImpl.isIE = true;
                        }
                        Control.ActiveXImpl.checkedIE = true;
                    }
                    return Control.ActiveXImpl.isIE;
                }
            }

            private Point LogPixels
            {
                get
                {
                    if (Control.ActiveXImpl.logPixels.IsEmpty)
                    {
                        Control.ActiveXImpl.logPixels = new Point();
                        IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.GetDC(System.Windows.Forms.NativeMethods.NullHandleRef);
                        Control.ActiveXImpl.logPixels.X = System.Windows.Forms.UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, ptr1), 0x58);
                        Control.ActiveXImpl.logPixels.Y = System.Windows.Forms.UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, ptr1), 90);
                        System.Windows.Forms.UnsafeNativeMethods.ReleaseDC(System.Windows.Forms.NativeMethods.NullHandleRef, new HandleRef(null, ptr1));
                    }
                    return Control.ActiveXImpl.logPixels;
                }
            }


            // Fields
            private short accelCount;
            private IntPtr accelTable;
            private BitVector32 activeXState;
            private static readonly int adjustingRect;
            private System.Windows.Forms.NativeMethods.COMRECT adjustRect;
            private ArrayList adviseList;
            private Control.AmbientProperty[] ambientProperties;
            private static System.Windows.Forms.NativeMethods.tagOLEVERB[] axVerbs;
            private static readonly int changingExtents;
            private static bool checkedIE;
            private System.Windows.Forms.UnsafeNativeMethods.IOleClientSite clientSite;
            private IntPtr clipRegion;
            private Control control;
            private IWindowTarget controlWindowTarget;
            private static readonly int eventsFrozen;
            private static int globalActiveXCount;
            private static readonly int hiMetricPerInch;
            private IntPtr hwndParent;
            private static readonly int inPlaceActive;
            private System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceFrame inPlaceFrame;
            private System.Windows.Forms.UnsafeNativeMethods.IOleInPlaceUIWindow inPlaceUiWindow;
            private static readonly int inPlaceVisible;
            private static readonly int isDirty;
            private static bool isIE;
            private static Point logPixels;
            private static readonly int saving;
            private static readonly int uiActive;
            private static readonly int uiDead;
            private static readonly int viewAdviseOnlyOnce;
            private static readonly int viewAdvisePrimeFirst;
            private IAdviseSink viewAdviseSink;

            // Nested Types
            private class PropertyBagStream : System.Windows.Forms.UnsafeNativeMethods.IPropertyBag
            {
                // Methods
                public PropertyBagStream()
                {
                    this.bag = new Hashtable();
                }

                [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
                internal void Read(System.Windows.Forms.UnsafeNativeMethods.IStream istream)
                {
                    Stream stream1 = new DataStreamFromComStream(istream);
                    byte[] buffer1 = new byte[0x1000];
                    int num1 = 0;
                    int num2 = stream1.Read(buffer1, num1, 0x1000);
                    for (int num3 = num2; num2 == 0x1000; num3 += num2)
                    {
                        byte[] buffer2 = new byte[buffer1.Length + 0x1000];
                        Array.Copy(buffer1, buffer2, buffer1.Length);
                        buffer1 = buffer2;
                        num1 += 0x1000;
                        num2 = stream1.Read(buffer1, num1, 0x1000);
                    }
                    stream1 = new MemoryStream(buffer1);
                    BinaryFormatter formatter1 = new BinaryFormatter();
                    try
                    {
                        this.bag = (Hashtable) formatter1.Deserialize(stream1);
                    }
                    catch (Exception exception1)
                    {
                        if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                        {
                            throw;
                        }
                        this.bag = new Hashtable();
                    }
                    catch
                    {
                        this.bag = new Hashtable();
                    }
                }

                [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
                int System.Windows.Forms.UnsafeNativeMethods.IPropertyBag.Read(string pszPropName, ref object pVar, System.Windows.Forms.UnsafeNativeMethods.IErrorLog pErrorLog)
                {
                    if (!this.bag.Contains(pszPropName))
                    {
                        return -2147024809;
                    }
                    pVar = this.bag[pszPropName];
                    return 0;
                }

                [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
                int System.Windows.Forms.UnsafeNativeMethods.IPropertyBag.Write(string pszPropName, ref object pVar)
                {
                    this.bag[pszPropName] = pVar;
                    return 0;
                }

                [SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
                internal void Write(System.Windows.Forms.UnsafeNativeMethods.IStream istream)
                {
                    Stream stream1 = new DataStreamFromComStream(istream);
                    BinaryFormatter formatter1 = new BinaryFormatter();
                    formatter1.Serialize(stream1, this.bag);
                }


                // Fields
                private Hashtable bag;
            }
        }

        private class ActiveXVerbEnum : System.Windows.Forms.UnsafeNativeMethods.IEnumOLEVERB
        {
            // Methods
            internal ActiveXVerbEnum(System.Windows.Forms.NativeMethods.tagOLEVERB[] verbs)
            {
                this.verbs = verbs;
                this.current = 0;
            }

            public void Clone(out System.Windows.Forms.UnsafeNativeMethods.IEnumOLEVERB ppenum)
            {
                ppenum = new Control.ActiveXVerbEnum(this.verbs);
            }

            public int Next(int celt, System.Windows.Forms.NativeMethods.tagOLEVERB rgelt, int[] pceltFetched)
            {
                int num1 = 0;
                if (celt != 1)
                {
                    celt = 1;
                }
                while ((celt > 0) && (this.current < this.verbs.Length))
                {
                    rgelt.lVerb = this.verbs[this.current].lVerb;
                    rgelt.lpszVerbName = this.verbs[this.current].lpszVerbName;
                    rgelt.fuFlags = this.verbs[this.current].fuFlags;
                    rgelt.grfAttribs = this.verbs[this.current].grfAttribs;
                    celt--;
                    this.current++;
                    num1++;
                }
                if (pceltFetched != null)
                {
                    pceltFetched[0] = num1;
                }
                if (celt != 0)
                {
                    return 1;
                }
                return 0;
            }

            public void Reset()
            {
                this.current = 0;
            }

            public int Skip(int celt)
            {
                if ((this.current + celt) < this.verbs.Length)
                {
                    this.current += celt;
                    return 0;
                }
                this.current = this.verbs.Length;
                return 1;
            }


            // Fields
            private int current;
            private System.Windows.Forms.NativeMethods.tagOLEVERB[] verbs;
        }

        private class AmbientProperty
        {
            // Methods
            internal AmbientProperty(string name, int dispID)
            {
                this.name = name;
                this.dispID = dispID;
                this.value = null;
                this.empty = true;
            }

            internal void ResetValue()
            {
                this.empty = true;
                this.value = null;
            }


            // Properties
            internal int DispID
            {
                get
                {
                    return this.dispID;
                }
            }

            internal bool Empty
            {
                get
                {
                    return this.empty;
                }
            }

            internal string Name
            {
                get
                {
                    return this.name;
                }
            }

            internal object Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;
                    this.empty = false;
                }
            }


            // Fields
            private int dispID;
            private bool empty;
            private string name;
            private object value;
        }

        private class AxSourcingSite : ISite, IServiceProvider
        {
            // Methods
            internal AxSourcingSite(IComponent component, System.Windows.Forms.UnsafeNativeMethods.IOleClientSite clientSite, string name)
            {
                this.component = component;
                this.clientSite = clientSite;
                this.name = name;
            }

            public object GetService(System.Type service)
            {
                object obj1 = null;
                if (service == typeof(HtmlDocument))
                {
                    System.Windows.Forms.UnsafeNativeMethods.IOleContainer container1;
                    int num1;
                    try
                    {
                        System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                        num1 = this.clientSite.GetContainer(out container1);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    if (!System.Windows.Forms.NativeMethods.Succeeded(num1) || !(container1 is System.Windows.Forms.UnsafeNativeMethods.IHTMLDocument))
                    {
                        return obj1;
                    }
                    if (this.shimManager == null)
                    {
                        this.shimManager = new HtmlShimManager();
                    }
                    return new HtmlDocument(this.shimManager, container1 as System.Windows.Forms.UnsafeNativeMethods.IHTMLDocument);
                }
                if (this.clientSite.GetType().IsAssignableFrom(service))
                {
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Demand();
                    obj1 = this.clientSite;
                }
                return obj1;
            }


            // Properties
            public IComponent Component
            {
                get
                {
                    return this.component;
                }
            }

            public IContainer Container
            {
                get
                {
                    return null;
                }
            }

            public bool DesignMode
            {
                get
                {
                    return false;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if ((value == null) || (this.name == null))
                    {
                        this.name = value;
                    }
                }
            }


            // Fields
            private System.Windows.Forms.UnsafeNativeMethods.IOleClientSite clientSite;
            private IComponent component;
            private string name;
            private HtmlShimManager shimManager;
        }

        [ComVisible(true)]
        public class ControlAccessibleObject : AccessibleObject
        {
            // Methods
            static ControlAccessibleObject()
            {
                Control.ControlAccessibleObject.oleAccAvailable = System.Windows.Forms.NativeMethods.InvalidIntPtr;
            }

            public ControlAccessibleObject(Control ownerControl)
            {
                this.handle = IntPtr.Zero;
                if (ownerControl == null)
                {
                    throw new ArgumentNullException("ownerControl");
                }
                this.ownerControl = ownerControl;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    this.Handle = ownerControl.Handle;
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }

            internal ControlAccessibleObject(Control ownerControl, int accObjId)
            {
                this.handle = IntPtr.Zero;
                if (ownerControl == null)
                {
                    throw new ArgumentNullException("ownerControl");
                }
                base.AccessibleObjectId = accObjId;
                this.ownerControl = ownerControl;
                System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
                try
                {
                    this.Handle = ownerControl.Handle;
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }

            public override int GetHelpTopic(out string fileName)
            {
                int num1 = 0;
                QueryAccessibilityHelpEventHandler handler1 = (QueryAccessibilityHelpEventHandler) this.Owner.Events[Control.EventQueryAccessibilityHelp];
                if (handler1 == null)
                {
                    return base.GetHelpTopic(out fileName);
                }
                QueryAccessibilityHelpEventArgs args1 = new QueryAccessibilityHelpEventArgs();
                handler1(this.Owner, args1);
                fileName = args1.HelpNamespace;
                if (!string.IsNullOrEmpty(fileName))
                {
                    System.Windows.Forms.IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
                }
                try
                {
                    num1 = int.Parse(args1.HelpKeyword, CultureInfo.InvariantCulture);
                }
                catch (Exception exception1)
                {
                    if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                    {
                        throw;
                    }
                }
                catch
                {
                }
                return num1;
            }

            internal override bool GetSysChild(AccessibleNavigation navdir, out AccessibleObject accessibleObject)
            {
                accessibleObject = null;
                Control control1 = this.ownerControl.ParentInternal;
                int num1 = -1;
                Control[] controlArray1 = null;
                switch (navdir)
                {
                    case AccessibleNavigation.Next:
                    {
                        if (base.IsNonClientObject && (control1 != null))
                        {
                            controlArray1 = control1.GetChildControlsInTabOrder(true);
                            num1 = Array.IndexOf<Control>(controlArray1, this.ownerControl);
                            if (num1 != -1)
                            {
                                num1++;
                            }
                        }
                        break;
                    }
                    case AccessibleNavigation.Previous:
                    {
                        if (base.IsNonClientObject && (control1 != null))
                        {
                            controlArray1 = control1.GetChildControlsInTabOrder(true);
                            num1 = Array.IndexOf<Control>(controlArray1, this.ownerControl);
                            if (num1 != -1)
                            {
                                num1--;
                            }
                        }
                        break;
                    }
                    case AccessibleNavigation.FirstChild:
                    {
                        if (base.IsClientObject)
                        {
                            controlArray1 = this.ownerControl.GetChildControlsInTabOrder(true);
                            num1 = 0;
                        }
                        break;
                    }
                    case AccessibleNavigation.LastChild:
                    {
                        if (base.IsClientObject)
                        {
                            controlArray1 = this.ownerControl.GetChildControlsInTabOrder(true);
                            num1 = controlArray1.Length - 1;
                        }
                        break;
                    }
                }
                if ((controlArray1 == null) || (controlArray1.Length == 0))
                {
                    return false;
                }
                if ((num1 >= 0) && (num1 < controlArray1.Length))
                {
                    accessibleObject = controlArray1[num1].NcAccessibilityObject;
                }
                return true;
            }

            internal override int[] GetSysChildOrder()
            {
                if (this.ownerControl.GetStyle(ControlStyles.ContainerControl))
                {
                    return this.ownerControl.GetChildWindowsInTabOrder();
                }
                return base.GetSysChildOrder();
            }

            public void NotifyClients(AccessibleEvents accEvent)
            {
                System.Windows.Forms.UnsafeNativeMethods.NotifyWinEvent((int) accEvent, new HandleRef(this, this.Handle), -4, 0);
            }

            public void NotifyClients(AccessibleEvents accEvent, int childID)
            {
                System.Windows.Forms.UnsafeNativeMethods.NotifyWinEvent((int) accEvent, new HandleRef(this, this.Handle), -4, childID + 1);
            }

            public void NotifyClients(AccessibleEvents accEvent, int objectID, int childID)
            {
                System.Windows.Forms.UnsafeNativeMethods.NotifyWinEvent((int) accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
            }

            public override string ToString()
            {
                if (this.Owner != null)
                {
                    return ("ControlAccessibleObject: Owner = " + this.Owner.ToString());
                }
                return "ControlAccessibleObject: Owner = null";
            }


            // Properties
            public override string DefaultAction
            {
                get
                {
                    string text1 = this.ownerControl.AccessibleDefaultActionDescription;
                    if (text1 != null)
                    {
                        return text1;
                    }
                    return base.DefaultAction;
                }
            }

            public override string Description
            {
                get
                {
                    string text1 = this.ownerControl.AccessibleDescription;
                    if (text1 != null)
                    {
                        return text1;
                    }
                    return base.Description;
                }
            }

            public IntPtr Handle
            {
                get
                {
                    return this.handle;
                }
                set
                {
                    System.Windows.Forms.IntSecurity.UnmanagedCode.Demand();
                    if (this.handle != value)
                    {
                        this.handle = value;
                        if (Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero)
                        {
                            bool flag1 = false;
                            if (Control.ControlAccessibleObject.oleAccAvailable == System.Windows.Forms.NativeMethods.InvalidIntPtr)
                            {
                                Control.ControlAccessibleObject.oleAccAvailable = System.Windows.Forms.UnsafeNativeMethods.LoadLibrary("oleacc.dll");
                                flag1 = Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero;
                            }
                            if ((this.handle != IntPtr.Zero) && (Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero))
                            {
                                base.UseStdAccessibleObjects(this.handle);
                            }
                            if (flag1)
                            {
                                System.Windows.Forms.UnsafeNativeMethods.FreeLibrary(new HandleRef(null, Control.ControlAccessibleObject.oleAccAvailable));
                            }
                        }
                    }
                }
            }

            public override string Help
            {
                get
                {
                    QueryAccessibilityHelpEventHandler handler1 = (QueryAccessibilityHelpEventHandler) this.Owner.Events[Control.EventQueryAccessibilityHelp];
                    if (handler1 != null)
                    {
                        QueryAccessibilityHelpEventArgs args1 = new QueryAccessibilityHelpEventArgs();
                        handler1(this.Owner, args1);
                        return args1.HelpString;
                    }
                    return base.Help;
                }
            }

            public override string KeyboardShortcut
            {
                get
                {
                    char ch1 = WindowsFormsUtils.GetMnemonic(this.TextLabel, false);
                    if (ch1 != '\0')
                    {
                        return ("Alt+" + ch1);
                    }
                    return null;
                }
            }

            public override string Name
            {
                get
                {
                    string text1 = this.ownerControl.AccessibleName;
                    if (text1 != null)
                    {
                        return text1;
                    }
                    return WindowsFormsUtils.TextWithoutMnemonics(this.TextLabel);
                }
                set
                {
                    this.ownerControl.AccessibleName = value;
                }
            }

            public Control Owner
            {
                get
                {
                    return this.ownerControl;
                }
            }

            public override AccessibleObject Parent
            {
                [SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return base.Parent;
                }
            }

            private Label PreviousLabel
            {
                get
                {
                    Control control1 = this.Owner.ParentInternal;
                    if (control1 != null)
                    {
                        ContainerControl control2 = control1.GetContainerControlInternal() as ContainerControl;
                        if (control2 == null)
                        {
                            return null;
                        }
                        for (Control control3 = control2.GetNextControl(this.Owner, false); control3 != null; control3 = control2.GetNextControl(control3, false))
                        {
                            if (control3 is Label)
                            {
                                return (control3 as Label);
                            }
                            if (control3.Visible && control3.TabStop)
                            {
                                break;
                            }
                        }
                    }
                    return null;
                }
            }

            public override AccessibleRole Role
            {
                get
                {
                    AccessibleRole role1 = this.ownerControl.AccessibleRole;
                    if (role1 != AccessibleRole.Default)
                    {
                        return role1;
                    }
                    return base.Role;
                }
            }

            internal string TextLabel
            {
                get
                {
                    if (this.ownerControl.GetStyle(ControlStyles.UseTextForAccessibility))
                    {
                        string text1 = this.ownerControl.Text;
                        if (!string.IsNullOrEmpty(text1))
                        {
                            return text1;
                        }
                    }
                    Label label1 = this.PreviousLabel;
                    if (label1 != null)
                    {
                        string text2 = label1.Text;
                        if (!string.IsNullOrEmpty(text2))
                        {
                            return text2;
                        }
                    }
                    return null;
                }
            }


            // Fields
            private IntPtr handle;
            private static IntPtr oleAccAvailable;
            private Control ownerControl;
        }

        [ListBindable(false), ComVisible(false)]
        public class ControlCollection : ArrangedElementCollection, IList, ICollection, System.Collections.IEnumerable, ICloneable
        {
            // Methods
            public ControlCollection(Control owner)
            {
                this.lastAccessedIndex = -1;
                this.owner = owner;
            }

            public virtual void Add(Control value)
            {
                if (value != null)
                {
                    if (value.GetTopLevel())
                    {
                        throw new ArgumentException(System.Windows.Forms.SR.GetString("TopLevelControlAdd"));
                    }
                    if (this.owner.CreateThreadId != value.CreateThreadId)
                    {
                        throw new ArgumentException(System.Windows.Forms.SR.GetString("AddDifferentThreads"));
                    }
                    Control.CheckParentingCycle(this.owner, value);
                    if (value.parent == this.owner)
                    {
                        value.SendToBack();
                    }
                    else
                    {
                        if (value.parent != null)
                        {
                            value.parent.Controls.Remove(value);
                        }
                        base.InnerList.Add(value);
                        if (value.tabIndex == -1)
                        {
                            int num1 = 0;
                            for (int num2 = 0; num2 < (this.Count - 1); num2++)
                            {
                                int num3 = this[num2].TabIndex;
                                if (num1 <= num3)
                                {
                                    num1 = num3 + 1;
                                }
                            }
                            value.tabIndex = num1;
                        }
                        this.owner.SuspendLayout();
                        try
                        {
                            Control control1 = value.parent;
                            try
                            {
                                value.AssignParent(this.owner);
                            }
                            finally
                            {
                                if ((control1 != value.parent) && ((this.owner.state & 1) != 0))
                                {
                                    value.SetParentHandle(this.owner.InternalHandle);
                                    if (value.Visible)
                                    {
                                        value.CreateControl();
                                    }
                                }
                            }
                            value.InitLayout();
                        }
                        finally
                        {
                            this.owner.ResumeLayout(false);
                        }
                        LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
                        this.owner.OnControlAdded(new ControlEventArgs(value));
                    }
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual void AddRange(Control[] controls)
            {
                if (controls == null)
                {
                    throw new ArgumentNullException("controls");
                }
                if (controls.Length > 0)
                {
                    this.owner.SuspendLayout();
                    try
                    {
                        for (int num1 = 0; num1 < controls.Length; num1++)
                        {
                            this.Add(controls[num1]);
                        }
                    }
                    finally
                    {
                        this.owner.ResumeLayout(true);
                    }
                }
            }

            public virtual void Clear()
            {
                this.owner.SuspendLayout();
                CommonProperties.xClearAllPreferredSizeCaches(this.owner);
                try
                {
                    while (this.Count != 0)
                    {
                        this.RemoveAt(this.Count - 1);
                    }
                }
                finally
                {
                    this.owner.ResumeLayout();
                }
            }

            public bool Contains(Control control)
            {
                return base.InnerList.Contains(control);
            }

            public virtual bool ContainsKey(string key)
            {
                return this.IsValidIndex(this.IndexOfKey(key));
            }

            public Control[] Find(string key, bool searchAllChildren)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key", System.Windows.Forms.SR.GetString("FindKeyMayNotBeEmptyOrNull"));
                }
                ArrayList list1 = this.FindInternal(key, searchAllChildren, this, new ArrayList());
                Control[] controlArray1 = new Control[list1.Count];
                list1.CopyTo(controlArray1, 0);
                return controlArray1;
            }

            private ArrayList FindInternal(string key, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
            {
                if ((controlsToLookIn == null) || (foundControls == null))
                {
                    return null;
                }
                try
                {
                    for (int num1 = 0; num1 < controlsToLookIn.Count; num1++)
                    {
                        if ((controlsToLookIn[num1] != null) && WindowsFormsUtils.SafeCompareStrings(controlsToLookIn[num1].Name, key, true))
                        {
                            foundControls.Add(controlsToLookIn[num1]);
                        }
                    }
                    if (!searchAllChildren)
                    {
                        return foundControls;
                    }
                    for (int num2 = 0; num2 < controlsToLookIn.Count; num2++)
                    {
                        if (((controlsToLookIn[num2] != null) && (controlsToLookIn[num2].Controls != null)) && (controlsToLookIn[num2].Controls.Count > 0))
                        {
                            foundControls = this.FindInternal(key, searchAllChildren, controlsToLookIn[num2].Controls, foundControls);
                        }
                    }
                }
                catch (Exception exception1)
                {
                    if (System.Windows.Forms.ClientUtils.IsSecurityOrCriticalException(exception1))
                    {
                        throw;
                    }
                }
                catch
                {
                }
                return foundControls;
            }

            public int GetChildIndex(Control child)
            {
                return this.GetChildIndex(child, true);
            }

            public virtual int GetChildIndex(Control child, bool throwException)
            {
                int num1 = this.IndexOf(child);
                if ((num1 == -1) && throwException)
                {
                    throw new ArgumentException(System.Windows.Forms.SR.GetString("ControlNotChild"));
                }
                return num1;
            }

            public override System.Collections.IEnumerator GetEnumerator()
            {
                return new ControlCollectionEnumerator(this);
            }

            public int IndexOf(Control control)
            {
                return base.InnerList.IndexOf(control);
            }

            public virtual int IndexOfKey(string key)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
                    {
                        return this.lastAccessedIndex;
                    }
                    for (int num1 = 0; num1 < this.Count; num1++)
                    {
                        if (WindowsFormsUtils.SafeCompareStrings(this[num1].Name, key, true))
                        {
                            this.lastAccessedIndex = num1;
                            return num1;
                        }
                    }
                    this.lastAccessedIndex = -1;
                }
                return -1;
            }

            private bool IsValidIndex(int index)
            {
                if (index >= 0)
                {
                    return (index < this.Count);
                }
                return false;
            }

            public virtual void Remove(Control value)
            {
                if ((value != null) && (value.ParentInternal == this.owner))
                {
                    value.SetParentHandle(IntPtr.Zero);
                    base.InnerList.Remove(value);
                    value.AssignParent(null);
                    LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
                    this.owner.OnControlRemoved(new ControlEventArgs(value));
                    ContainerControl control1 = this.owner.GetContainerControlInternal() as ContainerControl;
                    if (control1 != null)
                    {
                        control1.AfterControlRemoved(value);
                    }
                }
            }

            public void RemoveAt(int index)
            {
                this.Remove(this[index]);
            }

            public virtual void RemoveByKey(string key)
            {
                int num1 = this.IndexOfKey(key);
                if (this.IsValidIndex(num1))
                {
                    this.RemoveAt(num1);
                }
            }

            public virtual void SetChildIndex(Control child, int newIndex)
            {
                this.SetChildIndexInternal(child, newIndex);
            }

            internal virtual void SetChildIndexInternal(Control child, int newIndex)
            {
                if (child == null)
                {
                    throw new ArgumentNullException("child");
                }
                int num1 = this.GetChildIndex(child);
                if (num1 != newIndex)
                {
                    if ((newIndex >= this.Count) || (newIndex == -1))
                    {
                        newIndex = this.Count - 1;
                    }
                    base.MoveElement(child, num1, newIndex);
                    child.UpdateZOrder();
                    LayoutTransaction.DoLayout(this.owner, child, PropertyNames.ChildIndex);
                }
            }

            int IList.Add(object control)
            {
                if (control is Control)
                {
                    this.Add((Control) control);
                    return this.IndexOf((Control) control);
                }
                throw new ArgumentException(System.Windows.Forms.SR.GetString("ControlBadControl"), "control");
            }

            void IList.Remove(object control)
            {
                if (control is Control)
                {
                    this.Remove((Control) control);
                }
            }

            object ICloneable.Clone()
            {
                Control.ControlCollection collection1 = this.owner.CreateControlsInstance();
                collection1.InnerList.AddRange(this);
                return collection1;
            }


            // Properties
            public virtual Control this[string key]
            {
                get
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        int num1 = this.IndexOfKey(key);
                        if (this.IsValidIndex(num1))
                        {
                            return this[num1];
                        }
                    }
                    return null;
                }
            }

            public virtual Control this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        object[] objArray1 = new object[1] { index.ToString(CultureInfo.CurrentCulture) } ;
                        throw new ArgumentOutOfRangeException("index", System.Windows.Forms.SR.GetString("IndexOutOfRange", objArray1));
                    }
                    return (Control) base.InnerList[index];
                }
            }

            public Control Owner
            {
                get
                {
                    return this.owner;
                }
            }


            // Fields
            private int lastAccessedIndex;
            private Control owner;

            // Nested Types
            private class ControlCollectionEnumerator : System.Collections.IEnumerator
            {
                // Methods
                public ControlCollectionEnumerator(Control.ControlCollection controls)
                {
                    this.controls = controls;
                    this.originalCount = controls.Count;
                    this.current = -1;
                }

                public bool MoveNext()
                {
                    if ((this.current < (this.controls.Count - 1)) && (this.current < (this.originalCount - 1)))
                    {
                        this.current++;
                        return true;
                    }
                    return false;
                }

                public void Reset()
                {
                    this.current = -1;
                }


                // Properties
                public object Current
                {
                    get
                    {
                        if (this.current == -1)
                        {
                            return null;
                        }
                        return this.controls[this.current];
                    }
                }


                // Fields
                private Control.ControlCollection controls;
                private int current;
                private int originalCount;
            }
        }

        internal sealed class ControlNativeWindow : NativeWindow, IWindowTarget
        {
            // Methods
            internal ControlNativeWindow(Control control)
            {
                this.control = control;
                this.target = this;
            }

            internal Control GetControl()
            {
                return this.control;
            }

            internal void LockReference(bool locked)
            {
                if (locked)
                {
                    if (this.rootRef.IsAllocated)
                    {
                        return;
                    }
                    this.rootRef = GCHandle.Alloc(this.GetControl(), GCHandleType.Normal);
                }
                else if (this.rootRef.IsAllocated)
                {
                    this.rootRef.Free();
                }
            }

            protected override void OnHandleChange()
            {
                this.target.OnHandleChange(base.Handle);
            }

            public void OnHandleChange(IntPtr newHandle)
            {
                this.control.SetHandle(newHandle);
            }

            public void OnMessage(ref Message m)
            {
                this.control.WndProc(ref m);
            }

            protected override void OnThreadException(Exception e)
            {
                this.control.WndProcException(e);
            }

            protected override void WndProc(ref Message m)
            {
                int num1 = m.Msg;
                if (num1 != 0x200)
                {
                    if (num1 == 0x20a)
                    {
                        this.control.ResetMouseEventArgs();
                    }
                    else if (num1 == 0x2a3)
                    {
                        this.control.UnhookMouseEvent();
                    }
                }
                else if (!this.control.GetState(0x4000))
                {
                    this.control.HookMouseEvent();
                    if (!this.control.GetState(0x2000))
                    {
                        this.control.SendMessage(System.Windows.Forms.NativeMethods.WM_MOUSEENTER, 0, 0);
                    }
                    else
                    {
                        this.control.SetState(0x2000, false);
                    }
                }
                this.target.OnMessage(ref m);
            }


            // Properties
            internal IWindowTarget WindowTarget
            {
                get
                {
                    return this.target;
                }
                set
                {
                    this.target = value;
                }
            }


            // Fields
            private Control control;
            private GCHandle rootRef;
            internal IWindowTarget target;
        }

        private class ControlTabOrderComparer : IComparer
        {
            // Methods
            public ControlTabOrderComparer()
            {
            }

            int IComparer.Compare(object x, object y)
            {
                Control.ControlTabOrderHolder holder1 = (Control.ControlTabOrderHolder) x;
                Control.ControlTabOrderHolder holder2 = (Control.ControlTabOrderHolder) y;
                int num1 = holder1.newOrder - holder2.newOrder;
                if (num1 == 0)
                {
                    num1 = holder1.oldOrder - holder2.oldOrder;
                }
                return num1;
            }

        }

        private class ControlTabOrderHolder
        {
            // Methods
            internal ControlTabOrderHolder(int oldOrder, int newOrder, Control control)
            {
                this.oldOrder = oldOrder;
                this.newOrder = newOrder;
                this.control = control;
            }


            // Fields
            internal readonly Control control;
            internal readonly int newOrder;
            internal readonly int oldOrder;
        }

        private class ControlVersionInfo
        {
            // Methods
            internal ControlVersionInfo(Control owner)
            {
                this.owner = owner;
            }

            private FileVersionInfo GetFileVersionInfo()
            {
                if (this.versionInfo == null)
                {
                    string text1;
                    FileIOPermission permission1 = new FileIOPermission(PermissionState.None);
                    permission1.AllFiles = FileIOPermissionAccess.PathDiscovery;
                    permission1.Assert();
                    try
                    {
                        text1 = this.owner.GetType().Module.FullyQualifiedName;
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    new FileIOPermission(FileIOPermissionAccess.Read, text1).Assert();
                    try
                    {
                        this.versionInfo = FileVersionInfo.GetVersionInfo(text1);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                }
                return this.versionInfo;
            }


            // Properties
            internal string CompanyName
            {
                get
                {
                    if (this.companyName == null)
                    {
                        object[] objArray1 = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                        if ((objArray1 != null) && (objArray1.Length > 0))
                        {
                            this.companyName = ((AssemblyCompanyAttribute) objArray1[0]).Company;
                        }
                        if ((this.companyName == null) || (this.companyName.Length == 0))
                        {
                            this.companyName = this.GetFileVersionInfo().CompanyName;
                            if (this.companyName != null)
                            {
                                this.companyName = this.companyName.Trim();
                            }
                        }
                        if ((this.companyName == null) || (this.companyName.Length == 0))
                        {
                            string text1 = this.owner.GetType().Namespace;
                            if (text1 == null)
                            {
                                text1 = string.Empty;
                            }
                            int num1 = text1.IndexOf("/");
                            if (num1 != -1)
                            {
                                this.companyName = text1.Substring(0, num1);
                            }
                            else
                            {
                                this.companyName = text1;
                            }
                        }
                    }
                    return this.companyName;
                }
            }

            internal string ProductName
            {
                get
                {
                    if (this.productName == null)
                    {
                        object[] objArray1 = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                        if ((objArray1 != null) && (objArray1.Length > 0))
                        {
                            this.productName = ((AssemblyProductAttribute) objArray1[0]).Product;
                        }
                        if ((this.productName == null) || (this.productName.Length == 0))
                        {
                            this.productName = this.GetFileVersionInfo().ProductName;
                            if (this.productName != null)
                            {
                                this.productName = this.productName.Trim();
                            }
                        }
                        if ((this.productName == null) || (this.productName.Length == 0))
                        {
                            string text1 = this.owner.GetType().Namespace;
                            if (text1 == null)
                            {
                                text1 = string.Empty;
                            }
                            int num1 = text1.IndexOf(".");
                            if (num1 != -1)
                            {
                                this.productName = text1.Substring(num1 + 1);
                            }
                            else
                            {
                                this.productName = text1;
                            }
                        }
                    }
                    return this.productName;
                }
            }

            internal string ProductVersion
            {
                get
                {
                    if (this.productVersion == null)
                    {
                        object[] objArray1 = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                        if ((objArray1 != null) && (objArray1.Length > 0))
                        {
                            this.productVersion = ((AssemblyInformationalVersionAttribute) objArray1[0]).InformationalVersion;
                        }
                        if ((this.productVersion == null) || (this.productVersion.Length == 0))
                        {
                            this.productVersion = this.GetFileVersionInfo().ProductVersion;
                            if (this.productVersion != null)
                            {
                                this.productVersion = this.productVersion.Trim();
                            }
                        }
                        if (this.productVersion.Length == 0)
                        {
                            this.productVersion = "1.0.0.0";
                        }
                    }
                    return this.productVersion;
                }
            }


            // Fields
            private string companyName;
            private Control owner;
            private string productName;
            private string productVersion;
            private FileVersionInfo versionInfo;
        }

        internal sealed class FontHandleWrapper : MarshalByRefObject, IDisposable
        {
            // Methods
            internal FontHandleWrapper(Font font)
            {
                this.handle = font.ToHfont();
                System.Internal.HandleCollector.Add(this.handle, System.Windows.Forms.NativeMethods.CommonHandles.GDI);
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (this.handle != IntPtr.Zero)
                {
                    System.Windows.Forms.SafeNativeMethods.DeleteObject(new HandleRef(this, this.handle));
                    this.handle = IntPtr.Zero;
                }
            }

            ~FontHandleWrapper()
            {
                this.Dispose(false);
            }


            // Properties
            internal IntPtr Handle
            {
                get
                {
                    return this.handle;
                }
            }


            // Fields
            private IntPtr handle;
        }

        internal class ImeContext
        {
            // Methods
            private ImeContext()
            {
            }

            public static void Disable(IntPtr handle)
            {
                if (Control.ImeContext.IsOpen(handle))
                {
                    Control.ImeContext.SetOpenStatus(false, handle);
                }
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), System.Windows.Forms.NativeMethods.NullHandleRef);
                if (ptr1 != IntPtr.Zero)
                {
                    Control.originalImeContext = ptr1;
                }
            }

            public static void Enable(IntPtr handle)
            {
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
                if (ptr1 == IntPtr.Zero)
                {
                    if (Control.originalImeContext == IntPtr.Zero)
                    {
                        ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmCreateContext();
                        if (ptr1 != IntPtr.Zero)
                        {
                            System.Windows.Forms.UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, ptr1));
                        }
                    }
                    else
                    {
                        System.Windows.Forms.UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, Control.originalImeContext));
                    }
                }
                else
                {
                    System.Windows.Forms.UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, ptr1));
                }
                if (!Control.ImeContext.IsOpen(handle))
                {
                    Control.ImeContext.SetOpenStatus(true, handle);
                }
            }

            public static ImeMode GetImeMode(IntPtr handle)
            {
                ImeMode[] modeArray1 = Control.ImeModeConversion.InputLanguageTable;
                if (modeArray1 == Control.ImeModeConversion.UnsupportedTable)
                {
                    return ImeMode.Inherit;
                }
                ImeMode mode1 = ImeMode.NoControl;
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
                if (ptr1 == IntPtr.Zero)
                {
                    return ImeMode.Inherit;
                }
                if (!Control.ImeContext.IsOpen(handle))
                {
                    mode1 = modeArray1[3];
                }
                else
                {
                    int num1 = 0;
                    int num2 = 0;
                    System.Windows.Forms.UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, ptr1), ref num1, ref num2);
                    if ((num1 & 1) != 0)
                    {
                        if ((num1 & 2) != 0)
                        {
                            mode1 = ((num1 & 8) != 0) ? modeArray1[6] : modeArray1[7];
                        }
                        else
                        {
                            mode1 = ((num1 & 8) != 0) ? modeArray1[4] : modeArray1[5];
                        }
                    }
                    else
                    {
                        mode1 = ((num1 & 8) != 0) ? modeArray1[8] : modeArray1[9];
                    }
                }
                System.Windows.Forms.UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, ptr1));
                return mode1;
            }

            public static bool IsOpen(IntPtr handle)
            {
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
                bool flag1 = false;
                if (ptr1 != IntPtr.Zero)
                {
                    flag1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetOpenStatus(new HandleRef(null, ptr1));
                    System.Windows.Forms.UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, ptr1));
                }
                return flag1;
            }

            public static void SetImeStatus(ImeMode imeMode, IntPtr handle)
            {
                if (((imeMode != ImeMode.Inherit) && (imeMode != ImeMode.NoControl)) && (Control.ImeModeConversion.InputLanguageTable != Control.ImeModeConversion.UnsupportedTable))
                {
                    int num1 = 0;
                    int num2 = 0;
                    if (imeMode == Control.ImeModeConversion.InputLanguageImeModeDisabled)
                    {
                        Control.ImeContext.Disable(handle);
                    }
                    else
                    {
                        Control.ImeContext.Enable(handle);
                    }
                    switch (imeMode)
                    {
                        case ImeMode.NoControl:
                        case ImeMode.Disable:
                        {
                            return;
                        }
                        case ImeMode.On:
                        {
                            Control.ImeContext.SetOpenStatus(true, handle);
                            return;
                        }
                        case ImeMode.Off:
                        {
                            Control.ImeContext.SetOpenStatus(false, handle);
                            return;
                        }
                    }
                    if (Control.ImeModeConversion.ImeModeConversionBits.ContainsKey((ImeMode) imeMode))
                    {
                        Control.ImeModeConversion conversion1 = Control.ImeModeConversion.ImeModeConversionBits.get_Item((ImeMode) imeMode);
                        Control.ImeContext.SetOpenStatus(true, handle);
                        IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
                        System.Windows.Forms.UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, ptr1), ref num1, ref num2);
                        num1 |= conversion1.setBits;
                        num1 &= ~conversion1.clearBits;
                        System.Windows.Forms.UnsafeNativeMethods.ImmSetConversionStatus(new HandleRef(null, ptr1), num1, num2);
                        System.Windows.Forms.UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, ptr1));
                    }
                }
            }

            public static void SetOpenStatus(bool open, IntPtr handle)
            {
                IntPtr ptr1 = System.Windows.Forms.UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
                if (ptr1 != IntPtr.Zero)
                {
                    if (!System.Windows.Forms.UnsafeNativeMethods.ImmSetOpenStatus(new HandleRef(null, ptr1), open))
                    {
                        throw new Win32Exception();
                    }
                    if (!System.Windows.Forms.UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, ptr1)))
                    {
                        throw new Win32Exception();
                    }
                }
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ImeModeConversion
        {
            public const int ImeDisabled = 1;
            public const int ImeClosed = 3;
            public const int ImeNativeFullHiragana = 4;
            public const int ImeNativeHalfHiragana = 5;
            public const int ImeNativeFullKatakana = 6;
            public const int ImeNativeHalfKatakana = 7;
            public const int ImeAlphaFull = 8;
            public const int ImeAlphaHalf = 9;
            private static Dictionary<ImeMode, Control.ImeModeConversion> imeModeConversionBits;
            internal int setBits;
            internal int clearBits;
            private static ImeMode[] japaneseTable;
            private static ImeMode[] koreanTable;
            private static ImeMode[] chineseTable;
            private static ImeMode[] unsupportedTable;
            [ThreadStatic]
            private static ImeMode[] threadImeModeInputLangTable;
            public static ImeMode[] KoreanTable
            {
                get
                {
                    return Control.ImeModeConversion.koreanTable;
                }
            }
            public static ImeMode[] UnsupportedTable
            {
                get
                {
                    return Control.ImeModeConversion.unsupportedTable;
                }
            }
            public static void InvalidateInputLanguageTable()
            {
                Control.ImeModeConversion.threadImeModeInputLangTable = null;
            }
            public static ImeMode[] InputLanguageTable
            {
                get
                {
                    if (Control.ImeModeConversion.threadImeModeInputLangTable != null)
                    {
                        goto Label_00AC;
                    }
                    int num1 = ((int) ((long) InputLanguage.CurrentInputLanguage.Handle)) & 0xffff;
                    int num2 = num1;
                    if (num2 <= 0x804)
                    {
                        if (num2 == 0x404)
                        {
                            goto Label_007E;
                        }
                        switch (num2)
                        {
                            case 0x411:
                            {
                                Control.ImeModeConversion.threadImeModeInputLangTable = Control.ImeModeConversion.japaneseTable;
                                goto Label_00AC;
                            }
                            case 0x412:
                            {
                                goto Label_008A;
                            }
                            case 0x804:
                            {
                                goto Label_007E;
                            }
                        }
                        goto Label_00A2;
                    }
                    if (num2 <= 0xc04)
                    {
                        if (num2 == 0x812)
                        {
                            goto Label_008A;
                        }
                        if (num2 == 0xc04)
                        {
                            goto Label_007E;
                        }
                        goto Label_00A2;
                    }
                    if ((num2 != 0x1004) && (num2 != 0x1404))
                    {
                        goto Label_00A2;
                    }
                Label_007E:
                    Control.ImeModeConversion.threadImeModeInputLangTable = Control.ImeModeConversion.chineseTable;
                    goto Label_00AC;
                Label_008A:
                    Control.ImeModeConversion.threadImeModeInputLangTable = Control.ImeModeConversion.koreanTable;
                    goto Label_00AC;
                Label_00A2:
                    Control.ImeModeConversion.threadImeModeInputLangTable = Control.ImeModeConversion.unsupportedTable;
                Label_00AC:
                    return Control.ImeModeConversion.threadImeModeInputLangTable;
                }
            }
            public static Dictionary<ImeMode, Control.ImeModeConversion> ImeModeConversionBits
            {
                get
                {
                    if (Control.ImeModeConversion.imeModeConversionBits == null)
                    {
                        Control.ImeModeConversion conversion1;
                        Control.ImeModeConversion.imeModeConversionBits = new Dictionary<ImeMode, Control.ImeModeConversion>(7);
                        conversion1.setBits = 9;
                        conversion1.clearBits = 2;
                        Control.ImeModeConversion.imeModeConversionBits.Add(4, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 11;
                        conversion1.clearBits = 0;
                        Control.ImeModeConversion.imeModeConversionBits.Add(5, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 3;
                        conversion1.clearBits = 8;
                        Control.ImeModeConversion.imeModeConversionBits.Add(6, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 8;
                        conversion1.clearBits = 3;
                        Control.ImeModeConversion.imeModeConversionBits.Add(7, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 0;
                        conversion1.clearBits = 11;
                        Control.ImeModeConversion.imeModeConversionBits.Add(8, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 9;
                        conversion1.clearBits = 0;
                        Control.ImeModeConversion.imeModeConversionBits.Add(9, (Control.ImeModeConversion) conversion1);
                        conversion1.setBits = 1;
                        conversion1.clearBits = 8;
                        Control.ImeModeConversion.imeModeConversionBits.Add(10, (Control.ImeModeConversion) conversion1);
                    }
                    return Control.ImeModeConversion.imeModeConversionBits;
                }
            }
            internal static ImeMode InputLanguageImeModeDisabled
            {
                get
                {
                    ImeMode[] modeArray1 = Control.ImeModeConversion.InputLanguageTable;
                    if (modeArray1 == Control.ImeModeConversion.unsupportedTable)
                    {
                        return ImeMode.Disable;
                    }
                    return modeArray1[1];
                }
            }
            static ImeModeConversion()
            {
                ImeMode[] modeArray1 = new ImeMode[10] { ImeMode.Disable, ImeMode.Disable, ImeMode.Off, ImeMode.Off, ImeMode.Hiragana, ImeMode.Hiragana, ImeMode.Katakana, ImeMode.KatakanaHalf, ImeMode.AlphaFull, ImeMode.Alpha } ;
                Control.ImeModeConversion.japaneseTable = modeArray1;
                ImeMode[] modeArray2 = new ImeMode[10] { ImeMode.Disable, ImeMode.Disable, ImeMode.Alpha, ImeMode.Alpha, ImeMode.HangulFull, ImeMode.Hangul, ImeMode.HangulFull, ImeMode.Hangul, ImeMode.AlphaFull, ImeMode.Alpha } ;
                Control.ImeModeConversion.koreanTable = modeArray2;
                ImeMode[] modeArray3 = new ImeMode[10] { ImeMode.Off, ImeMode.Off, ImeMode.Off, ImeMode.Off, ImeMode.On, ImeMode.On, ImeMode.On, ImeMode.On, ImeMode.On, ImeMode.Off } ;
                Control.ImeModeConversion.chineseTable = modeArray3;
                Control.ImeModeConversion.unsupportedTable = new ImeMode[0];
            }
        }

        private class MetafileDCWrapper : IDisposable
        {
            // Methods
            internal MetafileDCWrapper(HandleRef hOriginalDC, Size size)
            {
                this.hBitmapDC = System.Windows.Forms.NativeMethods.NullHandleRef;
                this.hBitmap = System.Windows.Forms.NativeMethods.NullHandleRef;
                this.hOriginalBmp = System.Windows.Forms.NativeMethods.NullHandleRef;
                this.hMetafileDC = System.Windows.Forms.NativeMethods.NullHandleRef;
                if ((size.Width < 0) || (size.Height < 0))
                {
                    throw new ArgumentException("size", System.Windows.Forms.SR.GetString("ControlMetaFileDCWrapperSizeInvalid"));
                }
                this.hMetafileDC = hOriginalDC;
                this.destRect = new System.Windows.Forms.NativeMethods.RECT(0, 0, size.Width, size.Height);
                this.hBitmapDC = new HandleRef(this, System.Windows.Forms.UnsafeNativeMethods.CreateCompatibleDC(System.Windows.Forms.NativeMethods.NullHandleRef));
                int num1 = System.Windows.Forms.UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 14);
                int num2 = System.Windows.Forms.UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 12);
                this.hBitmap = new HandleRef(this, System.Windows.Forms.SafeNativeMethods.CreateBitmap(size.Width, size.Height, num1, num2, IntPtr.Zero));
                this.hOriginalBmp = new HandleRef(this, System.Windows.Forms.SafeNativeMethods.SelectObject(this.hBitmapDC, this.hBitmap));
            }

            private unsafe bool DICopy(HandleRef hdcDest, HandleRef hdcSrc, System.Windows.Forms.NativeMethods.RECT rect, bool bStretch)
            {
                bool flag1 = false;
                HandleRef ref1 = new HandleRef(this, System.Windows.Forms.SafeNativeMethods.CreateBitmap(1, 1, 1, 1, IntPtr.Zero));
                if (ref1.Handle != IntPtr.Zero)
                {
                    try
                    {
                        int num7;
                        int num8;
                        int num9;
                        int num10;
                        HandleRef ref2 = new HandleRef(this, System.Windows.Forms.SafeNativeMethods.SelectObject(hdcSrc, ref1));
                        if (ref2.Handle == IntPtr.Zero)
                        {
                            return flag1;
                        }
                        System.Windows.Forms.SafeNativeMethods.SelectObject(hdcSrc, ref2);
                        System.Windows.Forms.NativeMethods.BITMAP bitmap1 = new System.Windows.Forms.NativeMethods.BITMAP();
                        if (System.Windows.Forms.UnsafeNativeMethods.GetObject(ref2, Marshal.SizeOf(bitmap1), bitmap1) == 0)
                        {
                            return flag1;
                        }
                        System.Windows.Forms.NativeMethods.BITMAPINFO_FLAT bitmapinfo_flat1 = new System.Windows.Forms.NativeMethods.BITMAPINFO_FLAT();
                        bitmapinfo_flat1.bmiHeader_biSize = Marshal.SizeOf(typeof(System.Windows.Forms.NativeMethods.BITMAPINFOHEADER));
                        bitmapinfo_flat1.bmiHeader_biWidth = bitmap1.bmWidth;
                        bitmapinfo_flat1.bmiHeader_biHeight = bitmap1.bmHeight;
                        bitmapinfo_flat1.bmiHeader_biPlanes = 1;
                        bitmapinfo_flat1.bmiHeader_biBitCount = bitmap1.bmBitsPixel;
                        bitmapinfo_flat1.bmiHeader_biCompression = 0;
                        bitmapinfo_flat1.bmiHeader_biSizeImage = 0;
                        bitmapinfo_flat1.bmiHeader_biXPelsPerMeter = 0;
                        bitmapinfo_flat1.bmiHeader_biYPelsPerMeter = 0;
                        bitmapinfo_flat1.bmiHeader_biClrUsed = 0;
                        bitmapinfo_flat1.bmiHeader_biClrImportant = 0;
                        bitmapinfo_flat1.bmiColors = new byte[0x400];
                        long num2 = 1 << ((bitmap1.bmBitsPixel * bitmap1.bmPlanes) & 0x1f);
                        if (num2 <= 0x100)
                        {
                            byte[] buffer1 = new byte[Marshal.SizeOf(typeof(System.Windows.Forms.NativeMethods.PALETTEENTRY)) * 0x100];
                            System.Windows.Forms.SafeNativeMethods.GetSystemPaletteEntries(hdcSrc, 0, (int) num2, buffer1);
                            try
                            {
                                byte[] buffer3;
                                if (((buffer3 = bitmapinfo_flat1.bmiColors) == null) || (buffer3.Length == 0))
                                {
                                    fixed (byte* local1 = IntPtr.Zero)
                                    {
                                    }
                                }
                                try
                                {
                                    byte[] buffer4;
                                    if (((buffer4 = buffer1) == null) || (buffer4.Length == 0))
                                    {
                                        fixed (byte* local2 = IntPtr.Zero)
                                        {
                                        }
                                    }
                                    System.Windows.Forms.NativeMethods.RGBQUAD* rgbquadPtr1 = (System.Windows.Forms.NativeMethods.RGBQUAD*) local1;
                                    System.Windows.Forms.NativeMethods.PALETTEENTRY* paletteentryPtr1 = (System.Windows.Forms.NativeMethods.PALETTEENTRY*) local2;
                                    for (long num1 = 0; num1 < ((int) num2); num1++)
                                    {
                                        rgbquadPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.RGBQUAD))].rgbRed = paletteentryPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.PALETTEENTRY))].peRed;
                                        rgbquadPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.RGBQUAD))].rgbBlue = paletteentryPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.PALETTEENTRY))].peBlue;
                                        rgbquadPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.RGBQUAD))].rgbGreen = paletteentryPtr1[(int) (num1 * sizeof(System.Windows.Forms.NativeMethods.PALETTEENTRY))].peGreen;
                                    }
                                }
                                finally
                                {
                                    local2 = (byte*) IntPtr.Zero;
                                }
                            }
                            finally
                            {
                                local1 = (byte*) IntPtr.Zero;
                            }
                        }
                        long num3 = bitmap1.bmBitsPixel * bitmap1.bmWidth;
                        long num4 = (num3 + 7) / ((long) 8);
                        long num5 = num4 * bitmap1.bmHeight;
                        byte[] buffer2 = new byte[num5];
                        int num6 = System.Windows.Forms.SafeNativeMethods.GetDIBits(hdcSrc, ref2, 0, bitmap1.bmHeight, buffer2, ref bitmapinfo_flat1, 0);
                        if (num6 == 0)
                        {
                            return flag1;
                        }
                        if (bStretch)
                        {
                            num7 = rect.left;
                            num8 = rect.top;
                            num9 = rect.right - rect.left;
                            num10 = rect.bottom - rect.top;
                        }
                        else
                        {
                            num7 = rect.left;
                            num8 = rect.top;
                            num9 = bitmap1.bmWidth;
                            num10 = bitmap1.bmHeight;
                        }
                        int num11 = System.Windows.Forms.SafeNativeMethods.StretchDIBits(hdcDest, num7, num8, num9, num10, 0, 0, bitmap1.bmWidth, bitmap1.bmHeight, buffer2, ref bitmapinfo_flat1, 0, 0xcc0020);
                        if (num11 == -1)
                        {
                            return flag1;
                        }
                        flag1 = true;
                    }
                    finally
                    {
                        System.Windows.Forms.SafeNativeMethods.DeleteObject(ref1);
                    }
                }
                return flag1;
            }

            ~MetafileDCWrapper()
            {
                ((IDisposable) this).Dispose();
            }

            void IDisposable.Dispose()
            {
                if (((this.hBitmapDC.Handle != IntPtr.Zero) && (this.hMetafileDC.Handle != IntPtr.Zero)) && (this.hBitmap.Handle != IntPtr.Zero))
                {
                    try
                    {
                        bool flag1 = this.DICopy(this.hMetafileDC, this.hBitmapDC, this.destRect, true);
                        System.Windows.Forms.SafeNativeMethods.SelectObject(this.hBitmapDC, this.hOriginalBmp);
                        flag1 = System.Windows.Forms.SafeNativeMethods.DeleteObject(this.hBitmap);
                        flag1 = System.Windows.Forms.UnsafeNativeMethods.DeleteCompatibleDC(this.hBitmapDC);
                    }
                    finally
                    {
                        this.hBitmapDC = System.Windows.Forms.NativeMethods.NullHandleRef;
                        this.hBitmap = System.Windows.Forms.NativeMethods.NullHandleRef;
                        this.hOriginalBmp = System.Windows.Forms.NativeMethods.NullHandleRef;
                        GC.SuppressFinalize(this);
                    }
                }
            }


            // Properties
            internal IntPtr HDC
            {
                get
                {
                    return this.hBitmapDC.Handle;
                }
            }


            // Fields
            private System.Windows.Forms.NativeMethods.RECT destRect;
            private HandleRef hBitmap;
            private HandleRef hBitmapDC;
            private HandleRef hMetafileDC;
            private HandleRef hOriginalBmp;
        }

        private sealed class MultithreadSafeCallScope : IDisposable
        {
            // Methods
            internal MultithreadSafeCallScope()
            {
                if (Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall)
                {
                    Control.inCrossThreadSafeCall = true;
                    this.resultedInSet = true;
                }
                else
                {
                    this.resultedInSet = false;
                }
            }

            void IDisposable.Dispose()
            {
                if (this.resultedInSet)
                {
                    Control.inCrossThreadSafeCall = false;
                }
            }


            // Fields
            private bool resultedInSet;
        }

        private sealed class PrintPaintEventArgs : PaintEventArgs
        {
            // Methods
            internal PrintPaintEventArgs(System.Windows.Forms.Message m, IntPtr dc, Rectangle clipRect) : base(dc, clipRect)
            {
                this.m = m;
            }


            // Properties
            internal System.Windows.Forms.Message Message
            {
                get
                {
                    return this.m;
                }
            }


            // Fields
            private System.Windows.Forms.Message m;
        }

        private class ThreadMethodEntry : IAsyncResult
        {
            // Methods
            internal ThreadMethodEntry(Control caller, Delegate method, object[] args, bool synchronous, ExecutionContext executionContext)
            {
                this.invokeSyncObject = new object();
                this.caller = caller;
                this.method = method;
                this.args = args;
                this.exception = null;
                this.retVal = null;
                this.synchronous = synchronous;
                this.isCompleted = false;
                this.resetEvent = null;
                this.executionContext = executionContext;
            }

            internal void Complete()
            {
                lock (this.invokeSyncObject)
                {
                    this.isCompleted = true;
                    if (this.resetEvent != null)
                    {
                        this.resetEvent.Set();
                    }
                }
            }

            ~ThreadMethodEntry()
            {
                if (this.resetEvent != null)
                {
                    this.resetEvent.Close();
                }
            }


            // Properties
            public object AsyncState
            {
                get
                {
                    return null;
                }
            }

            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    if (this.resetEvent == null)
                    {
                        lock (this.invokeSyncObject)
                        {
                            if (this.resetEvent == null)
                            {
                                this.resetEvent = new ManualResetEvent(false);
                                if (this.isCompleted)
                                {
                                    this.resetEvent.Set();
                                }
                            }
                        }
                    }
                    return this.resetEvent;
                }
            }

            public bool CompletedSynchronously
            {
                get
                {
                    if (this.isCompleted && this.synchronous)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public bool IsCompleted
            {
                get
                {
                    return this.isCompleted;
                }
            }


            // Fields
            internal object[] args;
            internal Control caller;
            internal Exception exception;
            internal ExecutionContext executionContext;
            private object invokeSyncObject;
            private bool isCompleted;
            internal Delegate method;
            private ManualResetEvent resetEvent;
            internal object retVal;
            internal SynchronizationContext syncContext;
            internal bool synchronous;
        }
    }
}

