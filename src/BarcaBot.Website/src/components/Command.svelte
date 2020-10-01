<script>
    import { slide } from "svelte/transition";

    export let commandInfo = "";

    let dropdownVisible = false;
    let mainDiv;

    function onClick() {
        if (dropdownVisible) {
            dropdownVisible = false;
            mainDiv.classList.remove("rounded-b-none");
        } else {
            dropdownVisible = true;
            mainDiv.classList.add("rounded-b-none");
        }
    }
</script>

<div
    bind:this={mainDiv}
    on:click={onClick}
    class="bg-gray-900 py-4 px-6 cursor-pointer select-none rounded-lg">
    <div class="flex items-center ">
        <p class="text-2xl font-bold mr-4">={commandInfo.name}</p>
        <p class="text-gray-500">{commandInfo.about}</p>
    </div>
</div>

{#if dropdownVisible}
    <div
        transition:slide
        class="bg-gray-800 border border-t-0 rounded-lg rounded-t-none border-gray-900 py-4 px-6">
        <p class="font-bold">Usage</p>
        {#each commandInfo.usages as usage}
            <p class="font-light">={usage}</p>
        {/each}
        <p class="font-bold mt-2">Usage examples</p>
        {#each commandInfo.examples as example}
            <p class="font-light">={example}</p>
        {/each}
    </div>
{/if}
