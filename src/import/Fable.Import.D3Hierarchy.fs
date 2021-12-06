// ts2fable 0.7.1
module rec Fable.Import.D3Hierarchy
open System
open Fable.Core
open Fable.Core.JS
open System.Collections.Generic

type Array<'T> = System.Collections.Generic.IList<'T>

let [<Import("treemapSquarify","d3-hierarchy")>] treemapSquarify: RatioSquarifyTilingFactory = jsNative
let [<Import("treemapResquarify","d3-hierarchy")>] treemapResquarify: RatioSquarifyTilingFactory = jsNative

type [<AllowNullLiteral>] IExports =
    /// <summary>Constructs a root node from the specified hierarchical data.</summary>
    /// <param name="data">The root specified data.
    /// If *data* is a Map, it is implicitly converted to the entry [undefined, *data*],
    /// and the children accessor instead defaults to `(d) => Array.isArray(d) ? d[1] : null;`.</param>
    /// <param name="children">The specified children accessor function is invoked for each datum, starting with the root data,
    /// and must return an iterable of data representing the children, if any.
    /// If children is not specified, it defaults to: `(d) => d.children`.</param>
    abstract hierarchy: data: 'Datum * ?children: ('Datum -> IEnumerable<'Datum> option) -> HierarchyNode<'Datum>
    /// Constructs a new stratify operator with the default settings.
    abstract stratify: unit -> StratifyOperator<'Datum>
    /// Creates a new cluster layout with default settings.
    abstract cluster: unit -> ClusterLayout<'Datum>
    /// Creates a new tree layout with default settings.
    abstract tree: unit -> TreeLayout<'Datum>
    /// Creates a new treemap layout with default settings.
    abstract treemap: unit -> TreemapLayout<'Datum>
    /// Recursively partitions the specified nodes into an approximately-balanced binary tree,
    /// choosing horizontal partitioning for wide rectangles and vertical partitioning for tall rectangles.
    abstract treemapBinary: node: HierarchyRectangularNode<obj option> * x0: float * y0: float * x1: float * y1: float -> unit
    /// Divides the rectangular area specified by x0, y0, x1, y1 horizontally according the value of each of the specified node’s children.
    /// The children are positioned in order, starting with the left edge (x0) of the given rectangle.
    /// If the sum of the children’s values is less than the specified node’s value (i.e., if the specified node has a non-zero internal value),
    /// the remaining empty space will be positioned on the right edge (x1) of the given rectangle.
    abstract treemapDice: node: HierarchyRectangularNode<obj option> * x0: float * y0: float * x1: float * y1: float -> unit
    /// Divides the rectangular area specified by x0, y0, x1, y1 vertically according the value of each of the specified node’s children.
    /// The children are positioned in order, starting with the top edge (y0) of the given rectangle.
    /// If the sum of the children’s values is less than the specified node’s value (i.e., if the specified node has a non-zero internal value),
    /// the remaining empty space will be positioned on the bottom edge (y1) of the given rectangle.
    abstract treemapSlice: node: HierarchyRectangularNode<obj option> * x0: float * y0: float * x1: float * y1: float -> unit
    /// If the specified node has odd depth, delegates to treemapSlice; otherwise delegates to treemapDice.
    abstract treemapSliceDice: node: HierarchyRectangularNode<obj option> * x0: float * y0: float * x1: float * y1: float -> unit
    /// Creates a new partition layout with the default settings.
    abstract partition: unit -> PartitionLayout<'Datum>
    /// Creates a new pack layout with the default settings.
    abstract pack: unit -> PackLayout<'Datum>
    /// <summary>Packs the specified array of circles, each of which must have a `circle.r` property specifying the circle’s radius.
    /// The circles are positioned according to the front-chain packing algorithm by Wang et al.</summary>
    /// <param name="circles">The specified array of circles to pack.</param>
    abstract packSiblings: circles: ResizeArray<'Datum> -> Array<obj>
    /// <summary>Computes the smallest circle that encloses the specified array of circles, each of which must have
    /// a `circle.r` property specifying the circle’s radius, and `circle.x` and `circle.y` properties specifying the circle’s center.
    /// The enclosing circle is computed using the Matoušek-Sharir-Welzl algorithm. (See also Apollonius’ Problem.)</summary>
    /// <param name="circles">The specified array of circles to pack.</param>
    abstract packEnclose: circles: ResizeArray<'Datum> -> PackCircle

type [<AllowNullLiteral>] HierarchyLink<'Datum> =
    /// The source of the link.
    abstract source: HierarchyNode<'Datum> with get, set
    /// The target of the link.
    abstract target: HierarchyNode<'Datum> with get, set

type [<AllowNullLiteral>] HierarchyNode<'Datum> =
    /// The associated data, as specified to the constructor.
    abstract data: 'Datum with get, set
    /// Zero for the root node, and increasing by one for each descendant generation.
    abstract depth: float
    /// Zero for leaf nodes, and the greatest distance from any descendant leaf for internal nodes.
    abstract height: float
    /// The parent node, or null for the root node.
    abstract parent: HierarchyNode<'Datum> option with get, set
    /// An array of child nodes, if any; undefined for leaf nodes.
    abstract children: ResizeArray<HierarchyNode<'Datum>> option with get, set
    /// Aggregated numeric value as calculated by `sum(value)` or `count()`, if previously invoked.
    abstract value: float option
    /// Optional node id string set by `StratifyOperator`, if hierarchical data was created from tabular data using stratify().
    abstract id: string option
    /// Returns the array of ancestors nodes, starting with this node, then followed by each parent up to the root.
    abstract ancestors: unit -> ResizeArray<HierarchyNode<'Datum>>
    /// Returns the array of descendant nodes, starting with this node, then followed by each child in topological order.
    abstract descendants: unit -> ResizeArray<HierarchyNode<'Datum>>
    /// Returns the array of leaf nodes in traversal order; leaves are nodes with no children.
    abstract leaves: unit -> ResizeArray<HierarchyNode<'Datum>>
    /// <summary>Returns the first node in the hierarchy from this node for which the specified filter returns a truthy value. undefined if no such node is found.</summary>
    /// <param name="filter">Filter.</param>
    abstract find: filter: (HierarchyNode<'Datum> -> bool) -> HierarchyNode<'Datum> option
    /// <summary>Returns the shortest path through the hierarchy from this node to the specified target node.
    /// The path starts at this node, ascends to the least common ancestor of this node and the target node, and then descends to the target node.</summary>
    /// <param name="target">The target node.</param>
    abstract path: target: HierarchyNode<'Datum> -> ResizeArray<HierarchyNode<'Datum>>
    /// Returns an array of links for this node, where each link is an object that defines source and target properties.
    /// The source of each link is the parent node, and the target is a child node.
    abstract links: unit -> Array<HierarchyLink<'Datum>>
    /// <summary>Evaluates the specified value function for this node and each descendant in post-order traversal, and returns this node.
    /// The `node.value` property of each node is set to the numeric value returned by the specified function plus the combined value of all descendants.</summary>
    /// <param name="value">The value function is passed the node’s data, and must return a non-negative number.</param>
    abstract sum: value: ('Datum -> float) -> HierarchyNode<'Datum>
    /// Computes the number of leaves under this node and assigns it to `node.value`, and similarly for every descendant of node.
    /// If this node is a leaf, its count is one. Returns this node.
    abstract count: unit -> HierarchyNode<'Datum>
    /// <summary>Sorts the children of this node, if any, and each of this node’s descendants’ children,
    /// in pre-order traversal using the specified compare function, and returns this node.</summary>
    /// <param name="compare">The compare function is passed two nodes a and b to compare.
    /// If a should be before b, the function must return a value less than zero;
    /// if b should be before a, the function must return a value greater than zero;
    /// otherwise, the relative order of a and b are not specified. See `array.sort` for more.</param>
    abstract sort: compare: (HierarchyNode<'Datum> -> HierarchyNode<'Datum> -> float) -> HierarchyNode<'Datum>
    /// Returns an iterator over the node’s descendants in breadth-first order.
    abstract ``[Symbol.iterator]``: unit -> IEnumerator<HierarchyNode<'Datum>>
    /// <summary>Invokes the specified function for node and each descendant in breadth-first order,
    /// such that a given node is only visited if all nodes of lesser depth have already been visited,
    /// as well as all preceding nodes of the same depth.</summary>
    /// <param name="func">The specified function is passed the current descendant, the zero-based traversal index, and this node.</param>
    /// <param name="that">If that is specified, it is the this context of the callback.</param>
    abstract each: func: ('T -> HierarchyNode<'Datum> -> float -> HierarchyNode<'Datum> -> unit) * ?that: 'T -> HierarchyNode<'Datum>
    /// <summary>Invokes the specified function for node and each descendant in post-order traversal,
    /// such that a given node is only visited after all of its descendants have already been visited.</summary>
    /// <param name="func">The specified function is passed the current descendant, the zero-based traversal index, and this node.</param>
    /// <param name="that">If that is specified, it is the this context of the callback.</param>
    abstract eachAfter: func: ('T -> HierarchyNode<'Datum> -> float -> HierarchyNode<'Datum> -> unit) * ?that: 'T -> HierarchyNode<'Datum>
    /// <summary>Invokes the specified function for node and each descendant in pre-order traversal,
    /// such that a given node is only visited after all of its ancestors have already been visited.</summary>
    /// <param name="func">The specified function is passed the current descendant, the zero-based traversal index, and this node.</param>
    /// <param name="that">If that is specified, it is the this context of the callback.</param>
    abstract eachBefore: func: ('T -> HierarchyNode<'Datum> -> float -> HierarchyNode<'Datum> -> unit) * ?that: 'T -> HierarchyNode<'Datum>
    /// Return a deep copy of the subtree starting at this node. The returned deep copy shares the same data, however.
    /// The returned node is the root of a new tree; the returned node’s parent is always null and its depth is always zero.
    abstract copy: unit -> HierarchyNode<'Datum>

type [<AllowNullLiteral>] StratifyOperator<'Datum> =
    /// <summary>Generates a new hierarchy from the specified tabular data. Each node in the returned object has a shallow copy of the properties
    /// from the corresponding data object, excluding the following reserved properties: id, parentId, children.</summary>
    /// <param name="data">The root specified data.</param>
    [<Emit "$0($1...)">] abstract Invoke: data: ResizeArray<'Datum> -> HierarchyNode<'Datum>
    /// Returns the current id accessor, which defaults to: `(d) => d.id`.
    abstract id: unit -> ('Datum -> float -> ResizeArray<'Datum> -> U2<string, string> option)
    /// <summary>Sets the id accessor to the given function.
    /// The id accessor is invoked for each element in the input data passed to the stratify operator.
    /// The returned string is then used to identify the node's relationships in conjunction with the parent id.
    /// For leaf nodes, the id may be undefined, null or the empty string; otherwise, the id must be unique.</summary>
    /// <param name="id">The id accessor.</param>
    abstract id: id: ('Datum -> float -> ResizeArray<'Datum> -> U2<string, string> option) -> StratifyOperator<'Datum>
    /// Returns the current parent id accessor, which defaults to: `(d) => d.parentId`.
    abstract parentId: unit -> ('Datum -> float -> ResizeArray<'Datum> -> U2<string, string> option)
    /// <summary>Sets the parent id accessor to the given function.
    /// The parent id accessor is invoked for each element in the input data passed to the stratify operator.
    /// The returned string is then used to identify the node's relationships in conjunction with the id.
    /// For the root node, the parent id should be undefined, null or the empty string.
    /// There must be exactly one root node in the input data, and no circular relationships.</summary>
    /// <param name="parentId">The parent id accessor.</param>
    abstract parentId: parentId: ('Datum -> float -> ResizeArray<'Datum> -> U2<string, string> option) -> StratifyOperator<'Datum>

type [<AllowNullLiteral>] HierarchyPointLink<'Datum> =
    /// The source of the link.
    abstract source: HierarchyPointNode<'Datum> with get, set
    /// The target of the link.
    abstract target: HierarchyPointNode<'Datum> with get, set

type [<AllowNullLiteral>] HierarchyPointNode<'Datum> =
    inherit HierarchyNode<'Datum>
    /// The x-coordinate of the node.
    abstract x: float with get, set
    /// The y-coordinate of the node.
    abstract y: float with get, set
    /// Returns an array of links for this node, where each link is an object that defines source and target properties.
    /// The source of each link is the parent node, and the target is a child node.
    abstract links: unit -> Array<HierarchyPointLink<'Datum>>

type [<AllowNullLiteral>] ClusterLayout<'Datum> =
    /// <summary>Lays out the specified root hierarchy.
    /// You may want to call `root.sort` before passing the hierarchy to the cluster layout.</summary>
    /// <param name="root">The specified root hierarchy.</param>
    [<Emit "$0($1...)">] abstract Invoke: root: HierarchyNode<'Datum> -> HierarchyPointNode<'Datum>
    /// Returns the current layout size, which defaults to [1, 1]. A layout size of null indicates that a node size will be used instead.
    abstract size: unit -> float * float option
    /// <summary>Sets this cluster layout’s size to the specified [width, height] array and returns the cluster layout.
    /// The size represent an arbitrary coordinate system; for example, to produce a radial layout,
    /// a size of [360, radius] corresponds to a breadth of 360° and a depth of radius.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract size: size: float * float -> ClusterLayout<'Datum>
    /// Returns the current node size, which defaults to null. A node size of null indicates that a layout size will be used instead.
    abstract nodeSize: unit -> float * float option
    /// <summary>Sets this cluster layout’s node size to the specified [width, height] array and returns this cluster layout.
    /// When a node size is specified, the root node is always positioned at <0, 0>.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract nodeSize: size: float * float -> ClusterLayout<'Datum>
    /// Returns the current separation accessor, which defaults to: `(a, b) => a.parent == b.parent ? 1 : 2`.
    abstract separation: unit -> (HierarchyPointNode<'Datum> -> HierarchyPointNode<'Datum> -> float)
    /// <summary>Sets the separation accessor to the specified function and returns this cluster layout.
    /// The separation accessor is used to separate neighboring leaves.</summary>
    /// <param name="separation">The separation function is passed two leaves a and b, and must return the desired separation.
    /// The nodes are typically siblings, though the nodes may be more distantly related if the layout decides to place such nodes adjacent.</param>
    abstract separation: separation: (HierarchyPointNode<'Datum> -> HierarchyPointNode<'Datum> -> float) -> ClusterLayout<'Datum>

type [<AllowNullLiteral>] TreeLayout<'Datum> =
    /// <summary>Lays out the specified root hierarchy.
    /// You may want to call `root.sort` before passing the hierarchy to the tree layout.</summary>
    /// <param name="root">The specified root hierarchy.</param>
    [<Emit "$0($1...)">] abstract Invoke: root: HierarchyNode<'Datum> -> HierarchyPointNode<'Datum>
    /// Returns the current layout size, which defaults to [1, 1]. A layout size of null indicates that a node size will be used instead.
    abstract size: unit -> float * float option
    /// <summary>Sets this tree layout’s size to the specified [width, height] array and returns the tree layout.
    /// The size represent an arbitrary coordinate system; for example, to produce a radial layout,
    /// a size of [360, radius] corresponds to a breadth of 360° and a depth of radius.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract size: size: float * float -> TreeLayout<'Datum>
    /// Returns the current node size, which defaults to null. A node size of null indicates that a layout size will be used instead.
    abstract nodeSize: unit -> float * float option
    /// <summary>Sets this tree layout’s node size to the specified [width, height] array and returns this tree layout.
    /// When a node size is specified, the root node is always positioned at <0, 0>.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract nodeSize: size: float * float -> TreeLayout<'Datum>
    /// Returns the current separation accessor, which defaults to: `(a, b) => a.parent == b.parent ? 1 : 2`.
    abstract separation: unit -> (HierarchyPointNode<'Datum> -> HierarchyPointNode<'Datum> -> float)
    /// <summary>Sets the separation accessor to the specified function and returns this tree layout.
    /// The separation accessor is used to separate neighboring nodes.</summary>
    /// <param name="separation">The separation function is passed two nodes a and b, and must return the desired separation.
    /// The nodes are typically siblings, though the nodes may be more distantly related if the layout decides to place such nodes adjacent.</param>
    abstract separation: separation: (HierarchyPointNode<'Datum> -> HierarchyPointNode<'Datum> -> float) -> TreeLayout<'Datum>

type [<AllowNullLiteral>] HierarchyRectangularLink<'Datum> =
    /// The source of the link.
    abstract source: HierarchyRectangularNode<'Datum> with get, set
    /// The target of the link.
    abstract target: HierarchyRectangularNode<'Datum> with get, set

type [<AllowNullLiteral>] HierarchyRectangularNode<'Datum> =
    inherit HierarchyNode<'Datum>
    /// The left edge of the rectangle.
    abstract x0: float with get, set
    /// The top edge of the rectangle
    abstract y0: float with get, set
    /// The right edge of the rectangle.
    abstract x1: float with get, set
    /// The bottom edge of the rectangle.
    abstract y1: float with get, set
    /// Returns an array of links for this node, where each link is an object that defines source and target properties.
    /// The source of each link is the parent node, and the target is a child node.
    abstract links: unit -> Array<HierarchyRectangularLink<'Datum>>

type [<AllowNullLiteral>] TreemapLayout<'Datum> =
    /// <summary>Lays out the specified root hierarchy.
    /// You must call `root.sum` before passing the hierarchy to the treemap layout.
    /// You probably also want to call `root.sort` to order the hierarchy before computing the layout.</summary>
    /// <param name="root">The specified root hierarchy.</param>
    [<Emit "$0($1...)">] abstract Invoke: root: HierarchyNode<'Datum> -> HierarchyRectangularNode<'Datum>
    /// Returns the current tiling method, which defaults to `d3.treemapSquarify` with the golden ratio.
    abstract tile: unit -> (HierarchyRectangularNode<'Datum> -> float -> float -> float -> float -> unit)
    /// <summary>Sets the tiling method to the specified function and returns this treemap layout.</summary>
    /// <param name="tile">The specified tiling function.</param>
    abstract tile: tile: (HierarchyRectangularNode<'Datum> -> float -> float -> float -> float -> unit) -> TreemapLayout<'Datum>
    /// Returns the current size, which defaults to [1, 1].
    abstract size: unit -> float * float
    /// <summary>Sets this treemap layout’s size to the specified [width, height] array and returns this treemap layout.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract size: size: float * float -> TreemapLayout<'Datum>
    /// Returns the current rounding state, which defaults to false.
    abstract round: unit -> bool
    /// <summary>Enables or disables rounding according to the given boolean and returns this treemap layout.</summary>
    /// <param name="round">The specified boolean flag.</param>
    abstract round: round: bool -> TreemapLayout<'Datum>
    /// Returns the current inner padding function.
    abstract padding: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the inner and outer padding to the specified number and returns this treemap layout.</summary>
    /// <param name="padding">The specified padding value.</param>
    abstract padding: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the inner and outer padding to the specified function and returns this treemap layout.</summary>
    /// <param name="padding">The specified padding function.</param>
    abstract padding: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current inner padding function, which defaults to the constant zero.
    abstract paddingInner: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the inner padding to the specified number and returns this treemap layout.
    /// The inner padding is used to separate a node’s adjacent children.</summary>
    /// <param name="padding">The specified inner padding value.</param>
    abstract paddingInner: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the inner padding to the specified function and returns this treemap layout.
    /// The function is invoked for each node with children, being passed the current node.
    /// The inner padding is used to separate a node’s adjacent children.</summary>
    /// <param name="padding">The specified inner padding function.</param>
    abstract paddingInner: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current top padding function.
    abstract paddingOuter: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the top, right, bottom and left padding to the specified function and returns this treemap layout.</summary>
    /// <param name="padding">The specified padding outer value.</param>
    abstract paddingOuter: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the top, right, bottom and left padding to the specified function and returns this treemap layout.</summary>
    /// <param name="padding">The specified padding outer function.</param>
    abstract paddingOuter: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current top padding function, which defaults to the constant zero.
    abstract paddingTop: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the top padding to the specified number and returns this treemap layout.
    /// The top padding is used to separate the top edge of a node from its children.</summary>
    /// <param name="padding">The specified top padding value.</param>
    abstract paddingTop: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the top padding to the specified function and returns this treemap layout.
    /// The function is invoked for each node with children, being passed the current node.
    /// The top padding is used to separate the top edge of a node from its children.</summary>
    /// <param name="padding">The specified top padding function.</param>
    abstract paddingTop: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current right padding function, which defaults to the constant zero.
    abstract paddingRight: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the right padding to the specified number and returns this treemap layout.
    /// The right padding is used to separate the right edge of a node from its children.</summary>
    /// <param name="padding">The specified right padding value.</param>
    abstract paddingRight: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the right padding to the specified function and returns this treemap layout.
    /// The function is invoked for each node with children, being passed the current node.
    /// The right padding is used to separate the right edge of a node from its children.</summary>
    /// <param name="padding">The specified right padding function.</param>
    abstract paddingRight: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current bottom padding function, which defaults to the constant zero.
    abstract paddingBottom: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the bottom padding to the specified number and returns this treemap layout.
    /// The bottom padding is used to separate the bottom edge of a node from its children.</summary>
    /// <param name="padding">The specified bottom padding value.</param>
    abstract paddingBottom: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the bottom padding to the specified function and returns this treemap layout.
    /// The function is invoked for each node with children, being passed the current node.
    /// The bottom padding is used to separate the bottom edge of a node from its children.</summary>
    /// <param name="padding">The specified bottom padding function.</param>
    abstract paddingBottom: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>
    /// Returns the current left padding function, which defaults to the constant zero.
    abstract paddingLeft: unit -> (HierarchyRectangularNode<'Datum> -> float)
    /// <summary>Sets the left padding to the specified number and returns this treemap layout.
    /// The left padding is used to separate the left edge of a node from its children.</summary>
    /// <param name="padding">The specified left padding value.</param>
    abstract paddingLeft: padding: float -> TreemapLayout<'Datum>
    /// <summary>Sets the left padding to the specified function and returns this treemap layout.
    /// The function is invoked for each node with children, being passed the current node.
    /// The left padding is used to separate the left edge of a node from its children.</summary>
    /// <param name="padding">The specified left padding function.</param>
    abstract paddingLeft: padding: (HierarchyRectangularNode<'Datum> -> float) -> TreemapLayout<'Datum>

type [<AllowNullLiteral>] RatioSquarifyTilingFactory =
    [<Emit "$0($1...)">] abstract Invoke: node: HierarchyRectangularNode<obj option> * x0: float * y0: float * x1: float * y1: float -> unit
    /// <summary>Specifies the desired aspect ratio of the generated rectangles.
    /// Note that the orientation of the generated rectangles (tall or wide) is not implied by the ratio.
    /// Furthermore, the rectangles ratio are not guaranteed to have the exact specified aspect ratio.
    /// If not specified, the aspect ratio defaults to the golden ratio, φ = (1 + sqrt(5)) / 2, per Kong et al.</summary>
    /// <param name="ratio">The specified ratio value greater than or equal to one.</param>
    abstract ratio: ratio: float -> RatioSquarifyTilingFactory

type [<AllowNullLiteral>] PartitionLayout<'Datum> =
    /// <summary>Lays out the specified root hierarchy.
    /// You must call `root.sum` before passing the hierarchy to the partition layout.
    /// You probably also want to call `root.sort` to order the hierarchy before computing the layout.</summary>
    /// <param name="root">The specified root hierarchy.</param>
    [<Emit "$0($1...)">] abstract Invoke: root: HierarchyNode<'Datum> -> HierarchyRectangularNode<'Datum>
    /// Returns the current size, which defaults to [1, 1].
    abstract size: unit -> float * float
    /// <summary>Sets this partition layout’s size to the specified [width, height] array and returns this partition layout.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract size: size: float * float -> PartitionLayout<'Datum>
    /// Returns the current rounding state, which defaults to false.
    abstract round: unit -> bool
    /// <summary>Enables or disables rounding according to the given boolean and returns this partition layout.</summary>
    /// <param name="round">The specified boolean flag.</param>
    abstract round: round: bool -> PartitionLayout<'Datum>
    /// Returns the current padding, which defaults to zero.
    abstract padding: unit -> float
    /// <summary>Sets the padding to the specified number and returns this partition layout.
    /// The padding is used to separate a node’s adjacent children.</summary>
    /// <param name="padding">The specified padding value.</param>
    abstract padding: padding: float -> PartitionLayout<'Datum>

type [<AllowNullLiteral>] HierarchyCircularLink<'Datum> =
    /// The source of the link.
    abstract source: HierarchyCircularNode<'Datum> with get, set
    /// The target of the link.
    abstract target: HierarchyCircularNode<'Datum> with get, set

type [<AllowNullLiteral>] HierarchyCircularNode<'Datum> =
    inherit HierarchyNode<'Datum>
    /// The x-coordinate of the circle’s center.
    abstract x: float with get, set
    /// The y-coordinate of the circle’s center.
    abstract y: float with get, set
    /// The radius of the circle.
    abstract r: float with get, set
    /// Returns an array of links for this node, where each link is an object that defines source and target properties.
    /// The source of each link is the parent node, and the target is a child node.
    abstract links: unit -> Array<HierarchyCircularLink<'Datum>>

type [<AllowNullLiteral>] PackLayout<'Datum> =
    /// <summary>Lays out the specified root hierarchy.
    /// You must call `root.sum` before passing the hierarchy to the pack layout.
    /// You probably also want to call `root.sort` to order the hierarchy before computing the layout.</summary>
    /// <param name="root">The specified root hierarchy.</param>
    [<Emit "$0($1...)">] abstract Invoke: root: HierarchyNode<'Datum> -> HierarchyCircularNode<'Datum>
    /// Returns the current radius accessor, which defaults to null.
    abstract radius: unit -> (HierarchyCircularNode<'Datum> -> float) option
    /// <summary>Sets the pack layout’s radius accessor to the specified function and returns this pack layout.
    /// If the radius accessor is null, the radius of each leaf circle is derived from the leaf `node.value` (computed by `node.sum`);
    /// the radii are then scaled proportionally to fit the layout size.
    /// If the radius accessor is not null, the radius of each leaf circle is specified exactly by the function.</summary>
    /// <param name="radius">The specified radius accessor.</param>
    abstract radius: radius: (HierarchyCircularNode<'Datum> -> float) option -> PackLayout<'Datum>
    /// Returns the current size, which defaults to [1, 1].
    abstract size: unit -> float * float
    /// <summary>Sets this pack layout’s size to the specified [width, height] array and returns this pack layout.</summary>
    /// <param name="size">The specified two-element size array.</param>
    abstract size: size: float * float -> PackLayout<'Datum>
    /// Returns the current padding accessor, which defaults to the constant zero.
    abstract padding: unit -> (HierarchyCircularNode<'Datum> -> float)
    /// <summary>Sets this pack layout’s padding accessor to the specified number and returns this pack layout.
    /// Returns the current padding accessor, which defaults to the constant zero.
    /// 
    /// When siblings are packed, tangent siblings will be separated by approximately the specified padding;
    /// the enclosing parent circle will also be separated from its children by approximately the specified padding.
    /// If an explicit radius is not specified, the padding is approximate because a two-pass algorithm
    /// is needed to fit within the layout size: the circles are first packed without padding;
    /// a scaling factor is computed and applied to the specified padding; and lastly the circles are re-packed with padding.</summary>
    /// <param name="padding">The specified padding value.</param>
    abstract padding: padding: float -> PackLayout<'Datum>
    /// <summary>Sets this pack layout’s padding accessor to the specified function and returns this pack layout.
    /// Returns the current padding accessor, which defaults to the constant zero.
    /// 
    /// When siblings are packed, tangent siblings will be separated by approximately the specified padding;
    /// the enclosing parent circle will also be separated from its children by approximately the specified padding.
    /// If an explicit radius is not specified, the padding is approximate because a two-pass algorithm
    /// is needed to fit within the layout size: the circles are first packed without padding;
    /// a scaling factor is computed and applied to the specified padding; and lastly the circles are re-packed with padding.</summary>
    /// <param name="padding">The specified padding function.</param>
    abstract padding: padding: (HierarchyCircularNode<'Datum> -> float) -> PackLayout<'Datum>

type [<AllowNullLiteral>] PackRadius =
    /// The radius of the circle.
    abstract r: float with get, set
    /// The x-coordinate of the circle’s center.
    abstract x: float option with get, set
    /// The y-coordinate of the circle’s center.
    abstract y: float option with get, set

type [<AllowNullLiteral>] PackCircle =
    /// The radius of the circle.
    abstract r: float with get, set
    /// The x-coordinate of the circle’s center.
    abstract x: float with get, set
    /// The y-coordinate of the circle’s center.
    abstract y: float with get, set
