<script>
    export let link;
    export let text;
    export let margin;
</script>

<a
    href={link}
    class="border-2 border-gray-200 hover:bg-gray-200 hover:text-gray-800 duration-300 py-2 px-4 rounded-lg {margin}">
    {text}
</a>
